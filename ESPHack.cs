using System;
using UnityEngine;

// Token: 0x0200034F RID: 847
public class ESPHack : MonoBehaviour
{
	// Token: 0x06001674 RID: 5748 RVA: 0x000782B8 File Offset: 0x000764B8
	public void OnGUI()
	{
		if (this.UpdateSkinsTime < Time.time)
		{
			this.skins = (UnityEngine.Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[]);
			this.UpdateSkinsTime = Time.time + 1f;
		}
		if (ESPHack.PlayerESP)
		{
			this.ESP();
		}
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x00015ADC File Offset: 0x00013CDC
	public ESPHack()
	{
		ESPHack.LineMaterial.hideFlags = HideFlags.HideAndDontSave;
		ESPHack.LineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x0007830C File Offset: 0x0007650C
	private void ESP()
	{
		Rect rect = default(Rect);
		RenderSettings.fog = !ESPHack.NoFog;
		if (this.skins == null)
		{
			return;
		}
		for (int i = 0; i < this.skins.Length; i++)
		{
			Vector3 vector = this.skins[i].transform.position;
			Vector3 vector2 = vector;
			vector2.y += 1.8f;
			vector = Camera.main.WorldToScreenPoint(vector);
			vector2 = Camera.main.WorldToScreenPoint(vector2);
			if (vector.z > 0f && vector2.z > 0f)
			{
				Vector3 vector3 = GUIUtility.ScreenToGUIPoint(vector);
				vector3.y = (float)Screen.height - vector3.y;
				Vector3 vector4 = GUIUtility.ScreenToGUIPoint(vector2);
				vector4.y = (float)Screen.height - vector4.y;
				float num = Math.Abs(vector3.y - vector4.y) / 2.2f;
				float num2 = num / 2f;
				rect = new Rect(new Vector2(vector4.x - num2, vector4.y), new Vector2(num, vector3.y - vector4.y));
			}
			if (ESPHack.EspBox)
			{
				ESPHack.DrawRectangle(rect, ESPHack.Color, 4);
			}
			if (ESPHack.EspLine)
			{
				ESPHack.DrawLine(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f), new Vector3(rect.center.x, rect.center.y), ESPHack.Color, 1);
			}
		}
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x00015B01 File Offset: 0x00013D01
	public static void WHLoader()
	{
		ESPHack.load_object = new GameObject();
		ESPHack.load_object.AddComponent<ESPHack>();
		UnityEngine.Object.DontDestroyOnLoad(ESPHack.load_object);
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x000784B4 File Offset: 0x000766B4
	public static void DrawLine(Vector3 start, Vector3 end, Color color, int thickness)
	{
		ESPHack.LineMaterial.SetPass(0);
		if (thickness == 0)
		{
			return;
		}
		if (thickness == 1)
		{
			GL.Begin(1);
			GL.Color(color);
			GL.Vertex3(start.x, start.y, start.z);
			GL.Vertex3(end.x, end.y, end.z);
			GL.End();
			return;
		}
		thickness /= 2;
		GL.Begin(7);
		GL.Color(color);
		GL.Vertex3(start.x - (float)thickness, start.y - (float)thickness, start.z - (float)thickness);
		GL.Vertex3(start.x + (float)thickness, start.y + (float)thickness, start.z + (float)thickness);
		GL.Vertex3(end.x + (float)thickness, end.y + (float)thickness, end.z + (float)thickness);
		GL.Vertex3(end.x - (float)thickness, end.y - (float)thickness, end.z - (float)thickness);
		GL.End();
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000785AC File Offset: 0x000767AC
	public static void DrawRectangle(Rect rect, Color color, int thickness)
	{
		Vector3 vector = new Vector3(rect.x, rect.y, 0f);
		Vector3 vector2 = new Vector3(rect.x + rect.width, rect.y, 0f);
		Vector3 vector3 = new Vector3(rect.x + rect.width, rect.y + rect.height, 0f);
		Vector3 vector4 = new Vector3(rect.x, rect.y + rect.height, 0f);
		ESPHack.DrawLine(vector, vector2, color, thickness);
		ESPHack.DrawLine(vector2, vector3, color, thickness);
		ESPHack.DrawLine(vector3, vector4, color, thickness);
		ESPHack.DrawLine(vector4, vector, color, thickness);
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x00015B22 File Offset: 0x00013D22
	static ESPHack()
	{
	}

	// Token: 0x04000F7F RID: 3967
	private SkinnedMeshRenderer[] skins;

	// Token: 0x04000F80 RID: 3968
	public static GameObject load_object;

	// Token: 0x04000F81 RID: 3969
	public static bool PlayerESP;

	// Token: 0x04000F82 RID: 3970
	public static bool EspLine;

	// Token: 0x04000F83 RID: 3971
	public static bool EspBox;

	// Token: 0x04000F84 RID: 3972
	public static bool NoFog = true;

	// Token: 0x04000F85 RID: 3973
	private static readonly Material LineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));

	// Token: 0x04000F86 RID: 3974
	private float UpdateSkinsTime;

	// Token: 0x04000F87 RID: 3975
	public static Color Color = Color.green;
}
