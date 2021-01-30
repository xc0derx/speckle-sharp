﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace Speckle.ConnectorAutoCAD
{

  // this is used to store stream data - namedobjectdictionary persists after application is closed!
  public static class SpeckleStream
  {
    public static Document Doc => Application.DocumentManager.MdiActiveDocument;
    private static string SpeckleExtensionDictionary = "Speckle";


    // AutoCAD ogranizes information in the Named Object Dictionary (NOD) which is the root level dictionary
    // Users can create child dictionaries in the Named Object Dictionary for custom data.
    // Custom data is stored as XRecord key value entries of type (string, ResultBuffer).
    // ResultBuffers are TypedValue arrays, with the DxfCode of the input type as an integer.

    // Notes on disposing in AutoCAD: https://www.keanw.com/2008/06/cleaning-up-aft.html
    public static List<string> GetSpeckleStreams()
    {
      Database db = Doc.Database;
      List<string> streams = new List<string>();
      using (Transaction tr = db.TransactionManager.StartTransaction())
      {
        DBDictionary NOD = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
        if (NOD.Contains(SpeckleExtensionDictionary))
        {
          DBDictionary speckleDict = tr.GetObject(NOD.GetAt(SpeckleExtensionDictionary), OpenMode.ForRead) as DBDictionary;
          if (speckleDict != null && speckleDict.Count > 0)
          {
            foreach (DBDictionaryEntry entry in speckleDict)
            {
              Xrecord value = tr.GetObject(entry.Value, OpenMode.ForRead) as Xrecord;
              streams.Add(value.Data.AsArray()[0].Value as string);
            }
          }
        }
        tr.Commit();
      }
      return streams;
    }

    public static void AddSpeckleStream(string id, string stream)
    {
      Database db = Doc.Database;

      // document locks are used whenever you are modifying a doc from a modeless dialog, or in a command with the session flag
      using (DocumentLock l = Doc.LockDocument())
      {
        using (Transaction tr = db.TransactionManager.StartTransaction())
        {
          DBDictionary NOD = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
          DBDictionary speckleDict;
          if (NOD.Contains(SpeckleExtensionDictionary))
          {
            speckleDict = (DBDictionary)tr.GetObject(NOD.GetAt(SpeckleExtensionDictionary), OpenMode.ForWrite);
          }
          else
          {
            speckleDict = new DBDictionary();
            NOD.UpgradeOpen();
            NOD.SetAt(SpeckleExtensionDictionary, speckleDict);
            tr.AddNewlyCreatedDBObject(speckleDict, true);
          }
          Xrecord xRec = new Xrecord();
          xRec.Data = new ResultBuffer(new TypedValue(Convert.ToInt32(DxfCode.Text), stream));
          speckleDict.SetAt(id, xRec);
          tr.AddNewlyCreatedDBObject(xRec, true);
          tr.Commit();
        }
      }
    }

    public static void RemoveSpeckleStream(string id)
    {
      Database db = Doc.Database;
      // document locks are used whenever you are modifying a doc from a modeless dialog, or in a command with the session flag
      using (DocumentLock l = Doc.LockDocument())
      {
        using (Transaction tr = db.TransactionManager.StartTransaction())
        {
          DBDictionary NOD = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
          if (NOD.Contains(SpeckleExtensionDictionary))
          {
            DBDictionary speckleDict = (DBDictionary)tr.GetObject(NOD.GetAt(SpeckleExtensionDictionary), OpenMode.ForWrite);
            speckleDict.Remove(id);
          }
          tr.Commit();
        }
      }
    }

    public static void UpdateSpeckleStream(string id, string stream)
    {
      Database db = Doc.Database;
      // document locks are used whenever you are modifying a doc from a modeless dialog, or in a command with the session flag
      using (DocumentLock l = Doc.LockDocument())
      {
        using (Transaction tr = db.TransactionManager.StartTransaction())
        {
          DBDictionary NOD = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
          if (NOD.Contains(SpeckleExtensionDictionary))
          {
            DBDictionary speckleDict = (DBDictionary)tr.GetObject(NOD.GetAt(SpeckleExtensionDictionary), OpenMode.ForWrite);
            Xrecord xRec = new Xrecord();
            xRec.Data = new ResultBuffer(new TypedValue(Convert.ToInt32(DxfCode.Text), stream));
            speckleDict.SetAt(id, xRec);
            tr.AddNewlyCreatedDBObject(xRec, true);
          }
          tr.Commit();
        }
      }
    }
  }

  // this is for storing selection information - doc.userdata does NOT persist after application is closed!
  // stores the database entity handle NOT objectId! ObjectID does not persist once database leaves memory.
  public class UserData
  {
    public static Document Doc => Application.DocumentManager.MdiActiveDocument;

    // Specify a key under which we want to store our custom data
    const string SelectionKey = "Speckle"; 

    // public enum SelectionState { Current, Previous, None}; // add this back later if we need to keep track of various selection states, and change to dict if so

    public class SpeckleSelection
    {
      public List<string> Selection;
      public SpeckleSelection()
      {
        // intialize userdata dict entry if it doesnt already exist
        if (Doc.UserData[SelectionKey] == null)
          Doc.UserData[SelectionKey] = new List<string>();
        Selection = Doc.UserData[SelectionKey] as List<string>;
      }

      public void UpdateSelection(List<string> handles)
      {
        Selection = handles;
      }

      public void RemoveSelectionObjects(List<string> handles)
      {
        Selection = Selection.Where(o => !handles.Contains(o)).ToList();
      }
    }

    public static List<string> GetSpeckleSelection => new SpeckleSelection().Selection;

    public static void UpdateSpeckleSelection(List<string> handles)
    {
      SpeckleSelection sel = new SpeckleSelection();
      sel.UpdateSelection(handles);
      Doc.UserData[SelectionKey] = sel.Selection;
    }

    public static void RemoveFromSpeckleSelection(List<string> handles)
    {
      SpeckleSelection sel = new SpeckleSelection();
      sel.RemoveSelectionObjects(handles);
      Doc.UserData[SelectionKey] = sel.Selection;
    }
  }

  public static class ExtensionMethods
  {
    // this is in place because for whatever reason, autocad ObjectId.ToString() method returns "(idstring)" instead of "idstring".
    public static List<string> ToStrings(this ObjectId[] ids) => ids.Select(o => o.ToString().Trim(new char[] { '(', ')' })).ToList();

    // we are using handles instead of objectIds because handles persist and are saved with each file.
    // ObjectIds are unique to an application session (to assist with multipple document operations) but are recreated upon new sessions, not suitable for retrieving objects later.
    public static List<string> GetHandles(this SelectionSet selection)
    {
      Document Doc = Application.DocumentManager.MdiActiveDocument;
      List<string> handles = new List<string>();
      using (Transaction tr = Doc.TransactionManager.StartTransaction())
      {
        foreach (SelectedObject obj in selection)
        {
          if (obj != null)
          {
            Entity objEntity = tr.GetObject(obj.ObjectId, OpenMode.ForWrite) as Entity;
            if (objEntity != null)
              handles.Add(objEntity.Handle.ToString());
          }
        }
        tr.Commit();
      }
      return handles;
    }
  }
}