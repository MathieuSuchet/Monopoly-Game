using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;
using Newtonsoft.Json;

namespace MonopolySimulator;

[JsonObject(MemberSerialization.OptOut)]
public class PartieResultat
{
    public Dictionary<string, Stats> StatsMap { get; set; } = new Dictionary<string, Stats>();
}