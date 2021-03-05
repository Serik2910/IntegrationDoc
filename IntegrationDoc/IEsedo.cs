using IntegrationDoc.DocReceiver;
using IntegrationDoc.HedReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace IntegrationDoc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEsedo" in both code and config file together.
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [ServiceContractAttribute(Namespace = "http://qg.sync.isate.kz")]
    public interface IEsedo
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "send_to_esedo")]
        DocSender.DocumentResponse SendToESEDO(BaiterekMessageOut baiterekMessage);
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "send_to_esedo_new")]
        DocSender.DocumentResponse SendToESEDONew(BaiterekMessageOutNew baiterekMessage);
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "send_to_esedo_files")]
        void SendToESEDOFiles(BaiterekMessageFiles baiterekMessageFiles);
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "check_esedo_files")]
        string checkESEDOFiles(BaiterekMessageFilesRequest baiterekMessageFilesRequest);
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "download_from_esedo_files")]
        string downloadFromESEDOFiles(BaiterekMessageFilesRequest baiterekMessageFilesRequest);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "send_to_esedo_docOL")]
        DocSender.DocumentResponse SendToESEDODocOL(BaiterekMessagePEPOutOld orgMessagePEPOutOld);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "send_to_esedo_docOL_new")]
        DocSender.DocumentResponse SendToESEDODocOLNew(BaiterekMessagePEPOut orgMessagePEPOut);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "send_state_registered")]
        DocSender.DocumentResponse sendStateRegistered(EsedoStateMessage state);
        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Xml,
           UriTemplate = "send_state_not_valid")]
        DocSender.DocumentResponse sendStateNotValid(EsedoStateMessage state);
        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Xml,
           UriTemplate = "send_state_finished")]
        DocSender.DocumentResponse sendStateFinished(EsedoStateMessage state);
        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Xml,
           UriTemplate = "send_state_execution")]
        DocSender.DocumentResponse sendStateExecution(EsedoStateMessage state);

        [System.ServiceModel.OperationContractAttribute(Action = "http://qg.sync.isate.kz/QGServiceServ/SendUniversalMessage", 
            ReplyAction = "http://qg.sync.isate.kz/QGServiceServ/SendUniversalMessageResponse")]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "receive_from_esedoDOCQG")]
        qujatGatewayDelivered SendUniversalMessage(DocumentRequest request);

        //[OperationContractAttribute(Action = "http://qg.sync.isate.kz/QGServiceServ/SendDocAppeal", ReplyAction = "http://qg.sync.isate.kz/QGServiceServ/SendDocAppealResponse")]
        //[WebInvoke(Method = "POST",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "receive_from_esedoDOCAppeal")]
        //qujatGatewayDelivered SendDocAppeal(IntegrationDoc.DocReceiver.docAppeal data);

    }


    [ServiceContractAttribute(Namespace = "http://isate.kz/ensi/dictionary")]
    public interface IDict
    {
        [OperationContractAttribute(Action = "", ReplyAction = "")]
        [XmlSerializerFormatAttribute()]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "notify_DB_update")]
        NsiUpdate.GetDictionaryResponse GetDictionary(NsiUpdate.GetDictionaryRequest GetDictionaryRequest);
    }
    }
