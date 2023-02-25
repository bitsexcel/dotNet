

//using System.Runtime.Serialization;
namespace RequestTokenHacienda
{

    //[DataContract(Name = "Token", Namespace = "https://idp.comprobanteselectronicos.go.cr")]
    public class AdmAccessToken
    {
        //[DataMember]
        public string access_token { get; set; }
        //[DataMember]
        public string token_type { get; set; }
        //[DataMember]
        public string expires_in { get; set; }
        //[DataMember]
        public string scope { get; set; }
        //[DataMember]
        public string refresh_token { get; set; }
    }

}