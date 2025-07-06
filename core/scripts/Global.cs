using Godot;
using System;
using System.Collections.Generic;

public partial class Global : Node
{
	public static Dictionary<string, int> gemasPorMapa = new Dictionary<string, int>();

	public static string mapaAtual = "mapOne";
	
	public static int GetGemasDoMapaAtual()
	{
		if (!gemasPorMapa.ContainsKey(mapaAtual))
			gemasPorMapa[mapaAtual] = 0;

		return gemasPorMapa[mapaAtual];
	}

	public static void AddGema()
	{
		if (!gemasPorMapa.ContainsKey(mapaAtual))
			gemasPorMapa[mapaAtual] = 0;

		gemasPorMapa[mapaAtual]++;
	}

	public static void ResetGemasMapaAtual()
	{
		gemasPorMapa[mapaAtual] = 0;
	}
	
	public static int GetTotalGemas() {
		int total = 0;
		foreach (var kv in gemasPorMapa)
			total += kv.Value;

		return total;
	}
}
