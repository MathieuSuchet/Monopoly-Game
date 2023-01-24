using Newtonsoft.Json;

namespace MonopolySimulator;

[JsonObject(MemberSerialization.OptOut)]
public class Stats
{
    public int NbMaisonsAchetees { get; set; }
    public int NbMaisonsVendues { get; set; }
    
    public int NbCasesAchetees { get; set; }
    public int NbCasesVendues { get; set; }
    
    public int NbCasesParcourues { get; set; }
    
    public float ArgentGagne { get; set; }
    public float ArgentPerdu { get; set; }
    public int PassagesCaseDepart { get; set; }
}