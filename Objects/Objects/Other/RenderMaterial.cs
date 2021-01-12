using Speckle.Core.Models;

namespace Objects.Other
{
  /// <summary>
  /// Minimal physically based material DTO class. Based on references from
  /// https://threejs.org/docs/index.html#api/en/materials/MeshStandardMaterial
  /// Theoretically has equivalents in Unity and Unreal.
  /// 
  /// See: https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/PhysicallyBased/index.html
  /// And: https://blogs.unity3d.com/2014/10/29/physically-based-shading-in-unity-5-a-primer/
  /// </summary>
  public class RenderMaterial : Base
  {
    public int diffuse { get; set; }

    public int emissive { get; set; }

    public double opacity { get; set; } = 1;

    public double metalness { get; set; } = 0;

    public double roughness { get; set; } = 1;

    public RenderMaterial() { }
  }
}
