using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;
using IntegrationDoc.HedReference;
using IntegrationDoc.DocReceiver;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Web;
using System.Reflection;
using System.Linq.Expressions;
using System.Threading;
using System.Web.UI.WebControls.WebParts;
using IntegrationDoc.NsiSync;

using IntegrationDoc.NSI;
using IntegrationDoc.Models;

//using IntegrationDoc.ReceiverReference;

namespace IntegrationDoc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Esedo" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Esedo.svc or Esedo.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(Name = "QGServiceServ", ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Esedo : IEsedo,IDict
    {
        readonly object ThisLock = new object();
        public DocSender.DocumentResponse sendStateRegistered(EsedoStateMessage state)
        {
            DocSender.DocumentResponse response_to_baiterek;
            try
            {
                DocSender.DocumentPortClient documentPortClient = new DocSender.DocumentPortClient();
                DocSender.stateRegistered stateRegistered = new DocSender.stateRegistered()
                {
                    date = Convert.ToDateTime(state.o_date_start),
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = state.o_unid,
                        senderOrg = Const_esedo.baiterek_guid,
                        fromSpecified = true,
                        from = Const_esedo.baiterek_id,
                        href = state.o_unid,
                        performers = new long?[] { state.o_correspondent },
                        senderOrgSpecified = true
                    },
                    dateSpecified = true,
                    userUin = state.o_userUin_specified ? state.o_userUin : string.Empty,
                    idPortalInternalSpecified = state.o_id_portal_specified,
                    idPortalInternal = state.o_id_portal_specified ? long.Parse(state.o_id_portal) : 0,
                    regNo = state.o_reg_num,
                    secondSignNotifData = state.o_cms_base64
                };
                Const_esedo.QGClient qGClient = Const_esedo.qGClients.FirstOrDefault(p => p.ID == state.o_correspondent);
                DocSender.DocumentRequest documentRequest = new DocSender.DocumentRequest()
                {
                    requestDate = DateTime.Now,
                    sender = Const_esedo.sender,
                    receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                    data = stateRegistered
                };
                var serializer_ = new XmlSerializer(documentRequest.GetType(), new Type[] { typeof(DocSender.stateRegistered) });
                var sb_ = new StringBuilder();
                using (TextWriter writer = new StringWriter(sb_))
                {
                    serializer_.Serialize(writer, documentRequest);
                }
                string tmp_ = sb_.ToString();
                response_to_baiterek = documentPortClient.getDocument(documentRequest);
                if (state.o_docOL_specified && state.o_id_portal != "0" && state.o_id_portal != null)
                {
                    DocSender.stateRegistered stateRegistered_PEP = new DocSender.stateRegistered()
                    {
                        date = Convert.ToDateTime(state.o_date_start),
                        metadataSystem = new DocSender.metadataSystem()
                        {
                            activityId = state.o_unid,
                            senderOrg = Const_esedo.baiterek_guid,
                            fromSpecified = true,
                            from = Const_esedo.baiterek_id,
                            href = state.o_unid,
                            performers = new long?[] { state.o_id_portal.ToString().Length < 7 ? 17473609 : 4842363 },
                            senderOrgSpecified = true
                        },
                        dateSpecified = true,
                        userUin = state.o_userUin_specified ? state.o_userUin : string.Empty,
                        idPortalInternalSpecified = state.o_id_portal_specified,
                        idPortalInternal = state.o_id_portal_specified ? long.Parse(state.o_id_portal) : 0,
                        regNo = state.o_reg_num,
                        secondSignNotifData = state.o_cms_base64
                    };
                    documentRequest.data = stateRegistered_PEP;
                    response_to_baiterek = documentPortClient.getDocument(documentRequest);

                }
            }
            catch (Exception ex)
            {
                response_to_baiterek = new DocSender.DocumentResponse();
                response_to_baiterek.responseDate = DateTime.Now;
                response_to_baiterek.data_ = new DocSender.qujatGatewayDelivered()
                {
                    date = DateTime.Now,
                    status = 0,
                    error = ex.Message,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = state.o_unid,
                        from = Const_esedo.qujatgateway_id,
                        fromSpecified = true,
                        href = state.o_unid,
                        performers = new long?[] { Const_esedo.baiterek_id },
                        senderOrg = Const_esedo.qujatgateway_guid
                    }
                };
            }
            var serializer1 = new XmlSerializer(response_to_baiterek.GetType());
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer1.Serialize(writer, response_to_baiterek);
            }
            string tmp = sb.ToString();
            return response_to_baiterek;
        }
        public DocSender.DocumentResponse sendStateNotValid(EsedoStateMessage state)
        {
            DocSender.DocumentResponse response_to_baiterek;
            try
            {
                DocSender.DocumentPortClient documentPortClient = new DocSender.DocumentPortClient();
                DocSender.stateNotValid stateNot_Valid = new DocSender.stateNotValid()
                {
                    date = DateTime.Now,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = state.o_unid,
                        senderOrg = Const_esedo.baiterek_guid,
                        fromSpecified = true,
                        from = Const_esedo.baiterek_id,
                        href = state.o_unid,
                        performers = new long?[] { state.o_correspondent },
                        senderOrgSpecified = true
                    },
                    dateSpecified = true,
                    isValidReason = state.o_com,
                    secondSignNotifData = state.o_cms_base64,
                };
                Const_esedo.QGClient qGClient = Const_esedo.qGClients.FirstOrDefault(p => p.ID == state.o_correspondent);
                DocSender.DocumentRequest documentRequest = new DocSender.DocumentRequest()
                {
                    requestDate = DateTime.Now,
                    sender = Const_esedo.sender,
                    receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                    data = stateNot_Valid
                };
                response_to_baiterek = documentPortClient.getDocument(documentRequest);
                //throw new Exception("dfsdf");
            }
            catch (Exception ex)
            {
                response_to_baiterek = new DocSender.DocumentResponse();
                response_to_baiterek.responseDate = DateTime.Now;
                response_to_baiterek.data_ = new DocSender.qujatGatewayDelivered()
                {
                    date = DateTime.Now,
                    status = 0,
                    error = ex.Message,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = state.o_unid,
                        from = Const_esedo.qujatgateway_id,
                        fromSpecified = true,
                        href = state.o_unid,
                        performers = new long?[] { Const_esedo.baiterek_id },
                        senderOrg = Const_esedo.qujatgateway_guid
                    }
                };
            }
            var serializer1 = new XmlSerializer(response_to_baiterek.GetType());
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer1.Serialize(writer, response_to_baiterek);
            }
            string tmp = sb.ToString();
            return response_to_baiterek;
        }
        public DocSender.DocumentResponse sendStateExecution(EsedoStateMessage state)
        {
            DocSender.DocumentResponse response_to_baiterek;
            try
            {
                DocSender.DocumentPortClient documentPortClient = new DocSender.DocumentPortClient();
                DocSender.stateExecution stateExecution = new DocSender.stateExecution()
                {
                    date = DateTime.Now,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = state.o_unid,
                        senderOrg = Const_esedo.baiterek_guid,
                        fromSpecified = true,
                        from = Const_esedo.baiterek_id,
                        href = state.o_unid,
                        performers = new long?[] { state.o_correspondent },
                        senderOrgSpecified = true
                    },
                    dateSpecified = true,
                    userUin = state.o_userUin_specified ? state.o_userUin : string.Empty,
                    idPortalInternalSpecified = state.o_id_portal_specified,
                    idPortalInternal = state.o_id_portal_specified ? long.Parse(state.o_id_portal) : 0,
                    execDate = Convert.ToDateTime(state.o_date_end),
                    execDateSpecified = (state.o_date_end != null && state.o_date_end != "") ? true : false,
                    executive = state.o_performer,
                    executivePhone = state.o_phone
                };
                Const_esedo.QGClient qGClient = Const_esedo.qGClients.FirstOrDefault(p => p.ID == state.o_correspondent);
                DocSender.DocumentRequest documentRequest = new DocSender.DocumentRequest()
                {
                    requestDate = DateTime.Now,
                    sender = Const_esedo.sender,
                    receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                    data = stateExecution
                };
                response_to_baiterek = documentPortClient.getDocument(documentRequest);
                if (state.o_docOL_specified && state.o_id_portal != "0" && state.o_id_portal != null)
                {
                    DocSender.stateExecution stateExecution_PEP = new DocSender.stateExecution()
                    {
                        date = DateTime.Now,
                        metadataSystem = new DocSender.metadataSystem()
                        {
                            activityId = state.o_unid,
                            senderOrg = Const_esedo.baiterek_guid,
                            fromSpecified = true,
                            from = Const_esedo.baiterek_id,
                            href = state.o_unid,
                            performers = new long?[] { state.o_id_portal.ToString().Length < 7 ? 17473609 : 4842363 },
                            senderOrgSpecified = true
                        },
                        dateSpecified = true,
                        userUin = state.o_userUin_specified ? state.o_userUin : string.Empty,
                        idPortalInternalSpecified = state.o_id_portal_specified,
                        idPortalInternal = state.o_id_portal_specified ? long.Parse(state.o_id_portal) : 0,
                        execDate = Convert.ToDateTime(state.o_date_end),
                        execDateSpecified = (state.o_date_end != null && state.o_date_end != "") ? true : false,
                        executive = state.o_performer,
                        executivePhone = state.o_phone
                    };
                    documentRequest.data = stateExecution_PEP;
                    response_to_baiterek = documentPortClient.getDocument(documentRequest);
                }
            }
            catch (Exception ex)
            {
                response_to_baiterek = new DocSender.DocumentResponse();
                response_to_baiterek.responseDate = DateTime.Now;
                response_to_baiterek.data_ = new DocSender.qujatGatewayDelivered()
                {
                    date = DateTime.Now,
                    status = 0,
                    error = ex.Message,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = state.o_unid,
                        from = Const_esedo.qujatgateway_id,
                        fromSpecified = true,
                        href = state.o_unid,
                        performers = new long?[] { Const_esedo.baiterek_id },
                        senderOrg = Const_esedo.qujatgateway_guid
                    }
                };
            }
            var serializer1 = new XmlSerializer(response_to_baiterek.GetType());
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer1.Serialize(writer, response_to_baiterek);
            }
            string tmp = sb.ToString();
            return response_to_baiterek;
        }
        public DocSender.DocumentResponse sendStateFinished(EsedoStateMessage state)
        {
            DocSender.DocumentResponse response_to_baiterek;
            try
            {
                if (state.o_unid == "" || state.o_unid == null)
                {
                    throw new Exception("unid is empty");
                }
                DocSender.DocumentPortClient documentPortClient = new DocSender.DocumentPortClient();
                DocSender.stateFinished stateFinished = new DocSender.stateFinished()
                {

                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = state.o_unid,
                        senderOrg = Const_esedo.baiterek_guid,
                        fromSpecified = true,
                        from = Const_esedo.baiterek_id,
                        href = state.o_unid,
                        performers = new long?[] { state.o_correspondent },
                        senderOrgSpecified = true
                    },
                    author = state.o_performer,
                    realDateSpecified = true,
                    realDate = Convert.ToDateTime(state.o_real_date),
                    resultCode = state.o_result_code,
                    resultText = state.o_comment,
                    userUin = state.o_userUin_specified ? state.o_userUin : string.Empty,
                    idPortalInternalSpecified = state.o_id_portal_specified,
                    idPortalInternal = state.o_id_portal_specified ? long.Parse(state.o_id_portal) : 0
                };
                Const_esedo.QGClient qGClient = Const_esedo.qGClients.FirstOrDefault(p => p.ID == state.o_correspondent);
                DocSender.DocumentRequest documentRequest = new DocSender.DocumentRequest()
                {
                    requestDate = DateTime.Now,
                    sender = Const_esedo.sender,
                    receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                    data = stateFinished
                };
                response_to_baiterek = documentPortClient.getDocument(documentRequest);
                if (state.o_docOL_specified && state.o_id_portal != "0" && state.o_id_portal != null)
                {
                    DocSender.stateFinished stateFinished_PEP = new DocSender.stateFinished()
                    {

                        metadataSystem = new DocSender.metadataSystem()
                        {
                            activityId = state.o_unid,
                            senderOrg = Const_esedo.baiterek_guid,
                            fromSpecified = true,
                            from = Const_esedo.baiterek_id,
                            href = state.o_unid,
                            performers = new long?[] { state.o_id_portal.ToString().Length < 7 ? 17473609 : 4842363 },
                            senderOrgSpecified = true
                        },
                        author = state.o_performer,
                        realDateSpecified = true,
                        realDate = Convert.ToDateTime(state.o_real_date),
                        resultCode = state.o_result_code,
                        resultText = state.o_comment,
                        userUin = state.o_userUin_specified ? state.o_userUin : string.Empty,
                        idPortalInternalSpecified = state.o_id_portal_specified,
                        idPortalInternal = state.o_id_portal_specified ? long.Parse(state.o_id_portal) : 0
                    };
                    documentRequest.data = stateFinished_PEP;
                    response_to_baiterek = documentPortClient.getDocument(documentRequest);
                }
            }
            catch (Exception ex)
            {
                response_to_baiterek = new DocSender.DocumentResponse();
                response_to_baiterek.responseDate = DateTime.Now;
                response_to_baiterek.data_ = new DocSender.qujatGatewayDelivered()
                {
                    date = DateTime.Now,
                    status = 0,
                    error = ex.Message,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = state.o_unid,
                        from = 17466323,
                        fromSpecified = true,
                        href = state.o_unid,
                        performers = new long?[] { Const_esedo.baiterek_id },
                        senderOrg = 2294076
                    }
                };
            }
            var serializer1 = new XmlSerializer(response_to_baiterek.GetType());
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer1.Serialize(writer, response_to_baiterek);
            }
            string tmp = sb.ToString();
            return response_to_baiterek;
        }
        public DocSender.DocumentResponse SendToESEDO(BaiterekMessageOut baiterekMessageOut)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            UploadFileRequest[] uploads = new UploadFileRequest[baiterekMessageOut.files.Length];
            for (int i = 0; i < baiterekMessageOut.files.Length; i++)
            {
                UploadFileRequest upload = new UploadFileRequest
                {
                    content = System.Convert.FromBase64String(baiterekMessageOut.files[i].data),
                    fileProcessIdentifier = baiterekMessageOut.o_uuid,
                    lifeTime = 7*86400000,
                    lifeTimeSpecified = true,
                    name = baiterekMessageOut.files[i].name,
                    needToBeConfirmedSpecified = false,
                    mime = "application/octet-stream"
                };
                uploads[i] = upload;
            }
            GetMessageResponse hedResponce;
            DocSender.DocumentResponse response_to_baiterek;
            try
            {
                const int high = 7500000; //верхний порог размера файлов, а также максимальный размер пула
                const int total_high = 99600000;
                const int file_quantity_max = 80;
                if(uploads.Length> file_quantity_max)
                    throw new Exception($"Превышен максимальный лимит {file_quantity_max} кол-ва файлов с кол-вом {uploads.Length} файлов");
                List<(int a, int b)> tuppleList = new List<(int, int)>(); // список кортежей из пары значений (размер файла, индекс в upload)
                List<List<int>> group = new List<List<int>>(); // список списков разбиения  файлов по суммарному размеру <=high
                int total_volume = 0;
                for (int i = 0; i < uploads.Length; i++) // Забивка списка кортежей(размер, индекс) и проверка файлов по ограничителю 
                {
                    if (uploads[i].content.Length > high)
                        throw new Exception($"Превышен максимальный лимит {high} byte для файла {uploads[i].name} с размером {uploads[i].content.Length} byte");
                    total_volume += uploads[i].content.Length;
                    if (total_volume > total_high)
                        throw new Exception($"Превышен максимальный лимит для суммарного объема файлов {total_high} byte, т.к. на {i}-м файле объем стал {total_volume} byte");
                    tuppleList.Add((uploads[i].content.Length, i));
                }
                tuppleList.Sort();
                tuppleList.Reverse();
                while (tuppleList.Count > 0)
                {
                    int volume = (int)high, i = 0;
                    List<int> tempList = new List<int>();
                    tempList.Add(tuppleList[0].b);
                    volume -= tuppleList[0].a;
                    tuppleList.RemoveAt(0);
                    while ((tuppleList.Where(p => p.a <= volume).Count() != 0) && (i < tuppleList.Count))
                    {
                        if (tuppleList[i].a <= volume)
                        {
                            tempList.Add(tuppleList[i].b);
                            volume -= tuppleList[i].a;
                            tuppleList.RemoveAt(i);
                        }
                        else { i++; }
                    }
                    group.Add(tempList);
                }
                DocSender.attachment[] attachments_ = new DocSender.attachment[uploads.Length];
                int z = 0;
                foreach (var q in group)
                {
                    UploadFileRequest[] Sub_uploads = new UploadFileRequest[q.Count];
                    for (int i = 0; i < q.Count; i++)
                        Sub_uploads[i] = uploads[q[i]];
                    hedResponce = sendToHedFiles(Sub_uploads);
                    //JavaScriptSerializer serializer = new JavaScriptSerializer();
                    //dynamic a = hedResponce.response.responseData.data;
                    //string json2 = serializer.Serialize(hedResponce);
                    string json = JsonConvert.SerializeObject(hedResponce);
                    GetMessageResponse hedResponce1 = JsonConvert.DeserializeObject<GetMessageResponse>(json);
                    FileUploadResult[] fileUploadResults = hedResponce.response.responseData.data_.tempStorageResponse.uploadResponse.uploadFileResults;
                    
                    for (int i = 0; (i < fileUploadResults.Length) && (z< uploads.Length); i++, z++)
                    {
                        attachments_[z] = new DocSender.attachment() { fileIdentifier = fileUploadResults[i].fileIdentifier };
                    }
                }
                DocSender.DocumentPortClient documentPortClient = new DocSender.DocumentPortClient();
                DocSender.QGDocument qGDocument = new DocSender.QGDocument()
                {
                    attachments = attachments_,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = baiterekMessageOut.o_uuid,
                        senderOrg = Const_esedo.baiterek_guid,
                        fromSpecified = true,
                        from = Const_esedo.baiterek_id,
                        href = baiterekMessageOut.o_uuid,
                        performers = new long?[] { baiterekMessageOut.o_delivery_org_esedo }, // , 1629005
                        senderOrgSpecified = true
                    },
                    appendCount = baiterekMessageOut.o_in_amount,
                    authorNameKz = baiterekMessageOut.o_doc_author,
                    authorNameRu = baiterekMessageOut.o_doc_author,
                    carrierType = "1",
                    character = baiterekMessageOut.o_message_type,
                    characterSpecified = true,
                    controlTypeOuterCode = "",
                    controlTypeOuterNameKz = "",
                    controlTypeOuterNameRu = "",
                    description = baiterekMessageOut.o_description,
                    docDate = Convert.ToDateTime(baiterekMessageOut.o_doc_date),
                    docDateSpecified = true,
                    docKind = baiterekMessageOut.o_doc_type,
                    docKindSpecified = true,
                    docLang = baiterekMessageOut.o_doc_lang==null?"KZRU": baiterekMessageOut.o_doc_lang,
                    docNo = baiterekMessageOut.o_reg,
                    docNoR = baiterekMessageOut.o_docNoR_specified ? baiterekMessageOut.o_doc_NoR : "",
                    docRecPostKz = baiterekMessageOut.o_doc_rec_post,
                    docRecPostRu = baiterekMessageOut.o_doc_rec_post,
                    docToNumber = baiterekMessageOut.o_doc_to_number,
                    documentReceiverKz = baiterekMessageOut.o_document_receiver,
                    documentReceiverRu = baiterekMessageOut.o_document_receiver,
                    documentSectionId = "",
                    employeePhone = baiterekMessageOut.o_employee_phone,
                    //executionDate = Convert.ToDateTime(baiterekMessageOut.o_execution_date),
                    executor = baiterekMessageOut.o_exec,
                    outTime = DateTime.Now,
                    outTimeSpecified = true,
                    idPortalInternalSpecified = baiterekMessageOut.o_id_portal_spicified,
                    idPortalInternal = baiterekMessageOut.o_id_portal_spicified ? long.Parse(baiterekMessageOut.o_id_portal) : 0,
                    portalSign = baiterekMessageOut.o_id_portal_spicified ? baiterekMessageOut.o_sign_object_2 : "",
                    preparedDate = Convert.ToDateTime(baiterekMessageOut.o_date_event),
                    preparedDateSpecified = true,
                    resolutionText = baiterekMessageOut.o_resolution_text,
                    secondSignData = baiterekMessageOut.o_sign_object_2,
                    secondSignEnabled = "1",
                    sectionId = "",
                    sheetCount = baiterekMessageOut.o_doc_page_num,
                    signerNameKz = baiterekMessageOut.o_doc_author,
                    signerNameRu = baiterekMessageOut.o_doc_author,
                    userUin = baiterekMessageOut.o_useruin_specified ? baiterekMessageOut.o_useruin : "",
                    executionDateSpecified = false
                };
                Const_esedo.QGClient qGClient = Const_esedo.qGClients.FirstOrDefault(p => p.ID == baiterekMessageOut.o_delivery_org_esedo);
                DocSender.DocumentRequest documentRequest = new DocSender.DocumentRequest()
                {
                    requestDate = DateTime.Now,
                    sender = Const_esedo.sender,
                    receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                    data = qGDocument
                };
                response_to_baiterek = documentPortClient.getDocument(documentRequest);
                if (baiterekMessageOut.o_docol_specified)
                {
                    DocSender.QGDocument qGDocument_PEP = new DocSender.QGDocument()
                    {
                        attachments = attachments_,
                        metadataSystem = new DocSender.metadataSystem()
                        {
                            activityId = baiterekMessageOut.o_uuid,
                            senderOrg = Const_esedo.baiterek_guid,
                            fromSpecified = true,
                            from = Const_esedo.baiterek_id,
                            href = baiterekMessageOut.o_uuid,
                            performers = new long?[] { baiterekMessageOut.o_id_portal.ToString().Length < 7 ? 17473609 : 4842363 }, // , 1629005
                            senderOrgSpecified = true
                        },
                        appendCount = baiterekMessageOut.o_in_amount,
                        authorNameKz = baiterekMessageOut.o_doc_author,
                        authorNameRu = baiterekMessageOut.o_doc_author,
                        carrierType = "1",
                        character = baiterekMessageOut.o_message_type,
                        characterSpecified = true,
                        controlTypeOuterCode = "",
                        controlTypeOuterNameKz = "",
                        controlTypeOuterNameRu = "",
                        description = baiterekMessageOut.o_description,
                        docDate = Convert.ToDateTime(baiterekMessageOut.o_doc_date),
                        docDateSpecified = true,
                        docKind = baiterekMessageOut.o_doc_type,
                        docKindSpecified = true,
                        docLang = baiterekMessageOut.o_doc_lang == null ? "KZRU" : baiterekMessageOut.o_doc_lang,
                        docNo = baiterekMessageOut.o_reg,
                        docNoR = baiterekMessageOut.o_docNoR_specified ? baiterekMessageOut.o_doc_NoR : "",
                        docRecPostKz = baiterekMessageOut.o_doc_rec_post,
                        docRecPostRu = baiterekMessageOut.o_doc_rec_post,
                        docToNumber = baiterekMessageOut.o_doc_to_number,
                        documentReceiverKz = baiterekMessageOut.o_document_receiver,
                        documentReceiverRu = baiterekMessageOut.o_document_receiver,
                        documentSectionId = "",
                        employeePhone = baiterekMessageOut.o_employee_phone,
                        //executionDate = Convert.ToDateTime(baiterekMessageOut.o_execution_date),
                        executor = baiterekMessageOut.o_exec,
                        outTime = DateTime.Now,
                        outTimeSpecified = true,
                        idPortalInternalSpecified = baiterekMessageOut.o_id_portal_spicified,
                        idPortalInternal = baiterekMessageOut.o_id_portal_spicified ? long.Parse(baiterekMessageOut.o_id_portal) : 0,
                        portalSign = baiterekMessageOut.o_id_portal_spicified ? baiterekMessageOut.o_sign_object_2 : "",
                        preparedDate = Convert.ToDateTime(baiterekMessageOut.o_date_event),
                        preparedDateSpecified = true,
                        resolutionText = baiterekMessageOut.o_resolution_text,
                        secondSignData = baiterekMessageOut.o_sign_object_2,
                        secondSignEnabled = "1",
                        sectionId = "",
                        sheetCount = baiterekMessageOut.o_doc_page_num,
                        signerNameKz = baiterekMessageOut.o_doc_author,
                        signerNameRu = baiterekMessageOut.o_doc_author,
                        userUin = baiterekMessageOut.o_useruin_specified ? baiterekMessageOut.o_useruin : "",
                        executionDateSpecified = false
                    };
                    documentRequest.data = qGDocument_PEP;
                    response_to_baiterek = documentPortClient.getDocument(documentRequest);
                }
            }
            catch (Exception ex)
            {
                response_to_baiterek = new DocSender.DocumentResponse();
                response_to_baiterek.responseDate = DateTime.Now;
                response_to_baiterek.data_ = new DocSender.qujatGatewayDelivered()
                {
                    date = DateTime.Now,
                    status = 0,
                    error = ex.Message,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = baiterekMessageOut.o_uuid,
                        from = Const_esedo.qujatgateway_id,
                        fromSpecified = true,
                        href = baiterekMessageOut.o_uuid,
                        performers = new long?[] { Const_esedo.baiterek_id },
                        senderOrg = Const_esedo.qujatgateway_guid
                    }
                };
            }
            var serializer1 = new XmlSerializer(response_to_baiterek.GetType());
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer1.Serialize(writer, response_to_baiterek);
            }
            string tmp = sb.ToString();
            return response_to_baiterek;
        }
        public DocSender.DocumentResponse SendToESEDONew(BaiterekMessageOutNew baiterekMessageOutNew)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            DocSender.DocumentResponse response_to_baiterek;
            try
            {
                DocSender.attachment[] attachments_ = new DocSender.attachment[baiterekMessageOutNew.files.Length];
                for (int i = 0; i < attachments_.Length; i++)
                {
                    attachments_[i] = new DocSender.attachment() { fileIdentifier = baiterekMessageOutNew.files[i].id };
                }
                DocSender.DocumentPortClient documentPortClient = new DocSender.DocumentPortClient();
                DocSender.QGDocument qGDocument = new DocSender.QGDocument()
                {
                    attachments = attachments_,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = baiterekMessageOutNew.o_uuid,
                        senderOrg = Const_esedo.baiterek_guid,
                        fromSpecified = true,
                        from = Const_esedo.baiterek_id,
                        href = baiterekMessageOutNew.o_uuid,
                        performers = new long?[] { baiterekMessageOutNew.o_delivery_org_esedo }, // , 1629005
                        senderOrgSpecified = true
                    },
                    appendCount = baiterekMessageOutNew.o_in_amount,
                    authorNameKz = baiterekMessageOutNew.o_doc_author,
                    authorNameRu = baiterekMessageOutNew.o_doc_author,
                    carrierType = "1",
                    character = baiterekMessageOutNew.o_message_type,
                    characterSpecified = true,
                    controlTypeOuterCode = "",
                    controlTypeOuterNameKz = "",
                    controlTypeOuterNameRu = "",
                    description = baiterekMessageOutNew.o_description,
                    docDate = Convert.ToDateTime(baiterekMessageOutNew.o_doc_date),
                    docDateSpecified = true,
                    docNoR = baiterekMessageOutNew.o_docNoR_specified ? baiterekMessageOutNew.o_doc_NoR : "",
                    docKind = baiterekMessageOutNew.o_doc_type,
                    docKindSpecified = true,
                    docLang = baiterekMessageOutNew.o_doc_lang == null ? "KZRU" : baiterekMessageOutNew.o_doc_lang,
                    docNo = baiterekMessageOutNew.o_reg,
                    idPortalInternalSpecified = baiterekMessageOutNew.o_id_portal_specified,
                    idPortalInternal = baiterekMessageOutNew.o_id_portal_specified ? long.Parse(baiterekMessageOutNew.o_id_portal) : 0,
                    portalSign = "",
                    docRecPostKz = baiterekMessageOutNew.o_doc_rec_post,
                    docRecPostRu = baiterekMessageOutNew.o_doc_rec_post,
                    docToNumber = baiterekMessageOutNew.o_doc_to_number,
                    documentReceiverKz = baiterekMessageOutNew.o_document_receiver,
                    documentReceiverRu = baiterekMessageOutNew.o_document_receiver,
                    documentSectionId = "",
                    employeePhone = baiterekMessageOutNew.o_employee_phone,
                    //executionDate = Convert.ToDateTime(orgMessageOut.o_execution_date),
                    executor = baiterekMessageOutNew.o_exec,
                    outTime = DateTime.Now,
                    outTimeSpecified = true,
                    preparedDate = Convert.ToDateTime(baiterekMessageOutNew.o_date_event),
                    preparedDateSpecified = true,
                    resolutionText = baiterekMessageOutNew.o_resolution_text,
                    secondSignData = baiterekMessageOutNew.o_sign_object_2,
                    secondSignEnabled = "1",
                    sectionId = "",
                    sheetCount = baiterekMessageOutNew.o_doc_page_num,
                    signerNameKz = baiterekMessageOutNew.o_doc_author,
                    signerNameRu = baiterekMessageOutNew.o_doc_author,
                    userUin = baiterekMessageOutNew.o_useruin_specified ? baiterekMessageOutNew.o_useruin : "",
                    executionDateSpecified = false
                };
                Const_esedo.QGClient qGClient = Const_esedo.qGClients.FirstOrDefault(p => p.ID == baiterekMessageOutNew.o_delivery_org_esedo);
                DocSender.DocumentRequest documentRequest = new DocSender.DocumentRequest()
                {
                    requestDate = DateTime.Now,
                    sender = Const_esedo.sender,
                    receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                    data = qGDocument
                };
                response_to_baiterek = documentPortClient.getDocument(documentRequest);
                if (baiterekMessageOutNew.o_docol_specified && baiterekMessageOutNew.o_id_portal!="0" && baiterekMessageOutNew.o_id_portal_specified)
                {
                    DocSender.QGDocument qGDocument_PEP = new DocSender.QGDocument()
                    {
                        attachments = attachments_,
                        metadataSystem = new DocSender.metadataSystem()
                        {
                            activityId = baiterekMessageOutNew.o_uuid,
                            senderOrg = Const_esedo.baiterek_guid,
                            fromSpecified = true,
                            from = Const_esedo.baiterek_id,
                            href = baiterekMessageOutNew.o_uuid,
                            performers = new long?[] { baiterekMessageOutNew.o_id_portal.ToString().Length < 7 ? 17473609 : 4842363 },
                            senderOrgSpecified = true
                        },
                        appendCount = baiterekMessageOutNew.o_in_amount,
                        authorNameKz = baiterekMessageOutNew.o_doc_author,
                        authorNameRu = baiterekMessageOutNew.o_doc_author,
                        carrierType = "1",
                        character = baiterekMessageOutNew.o_message_type,
                        characterSpecified = true,
                        controlTypeOuterCode = "",
                        controlTypeOuterNameKz = "",
                        controlTypeOuterNameRu = "",
                        description = baiterekMessageOutNew.o_description,
                        docDate = Convert.ToDateTime(baiterekMessageOutNew.o_doc_date),
                        docDateSpecified = true,
                        docNoR = baiterekMessageOutNew.o_docNoR_specified ? baiterekMessageOutNew.o_doc_NoR : "",
                        docKind = baiterekMessageOutNew.o_doc_type,
                        docKindSpecified = true,
                        docLang = "KZRU",
                        docNo = baiterekMessageOutNew.o_reg,
                        idPortalInternalSpecified = baiterekMessageOutNew.o_id_portal_specified,
                        idPortalInternal = long.Parse(baiterekMessageOutNew.o_id_portal),
                        portalSign = "",
                        docRecPostKz = baiterekMessageOutNew.o_doc_rec_post,
                        docRecPostRu = baiterekMessageOutNew.o_doc_rec_post,
                        docToNumber = baiterekMessageOutNew.o_doc_to_number,
                        documentReceiverKz = baiterekMessageOutNew.o_document_receiver,
                        documentReceiverRu = baiterekMessageOutNew.o_document_receiver,
                        documentSectionId = "",
                        employeePhone = baiterekMessageOutNew.o_employee_phone,
                        executor = baiterekMessageOutNew.o_exec,
                        outTime = DateTime.Now,
                        outTimeSpecified = true,
                        preparedDate = Convert.ToDateTime(baiterekMessageOutNew.o_date_event),
                        preparedDateSpecified = true,
                        resolutionText = baiterekMessageOutNew.o_resolution_text,
                        secondSignData = baiterekMessageOutNew.o_sign_object_2,
                        secondSignEnabled = "1",
                        sectionId = "",
                        sheetCount = baiterekMessageOutNew.o_doc_page_num,
                        signerNameKz = baiterekMessageOutNew.o_doc_author,
                        signerNameRu = baiterekMessageOutNew.o_doc_author,
                        userUin = baiterekMessageOutNew.o_useruin_specified ? baiterekMessageOutNew.o_useruin : "",
                        executionDateSpecified = false
                    };
                    documentRequest.data = qGDocument_PEP;
                    response_to_baiterek = documentPortClient.getDocument(documentRequest);
                }
            }
            catch (Exception ex)
            {
                response_to_baiterek = new DocSender.DocumentResponse();
                response_to_baiterek.responseDate = DateTime.Now;
                response_to_baiterek.data_ = new DocSender.qujatGatewayDelivered()
                {
                    date = DateTime.Now,
                    status = 0,
                    error = ex.Message,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = baiterekMessageOutNew.o_uuid,
                        from = Const_esedo.qujatgateway_id,
                        fromSpecified = true,
                        href = baiterekMessageOutNew.o_uuid,
                        performers = new long?[] { Const_esedo.baiterek_id },
                        senderOrg = Const_esedo.qujatgateway_guid
                    }
                };
            }
            //var serializer1 = new XmlSerializer(response_to_baiterek.GetType());
            //var sb = new StringBuilder();
            //using (TextWriter writer = new StringWriter(sb))
            //{
            //    serializer1.Serialize(writer, response_to_baiterek);
            //}
            //string tmp = sb.ToString();
            return response_to_baiterek;
        }
       
        public DocSender.DocumentResponse SendToESEDODocOLNew(BaiterekMessagePEPOut baiterekMessagePEPOut)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            DocSender.DocumentResponse response_to_org;
            try
            {
                DocSender.attachment[] attachments_ = new DocSender.attachment[baiterekMessagePEPOut.files.Length];
                for (int i = 0; i < attachments_.Length; i++)
                {
                    attachments_[i] = new DocSender.attachment() { fileIdentifier = baiterekMessagePEPOut.files[i].id };
                }
                DocSender.DocumentPortClient documentPortClient = new DocSender.DocumentPortClient();
                DocSender.docOL docOL = new DocSender.docOL()
                {
                    attachments = attachments_,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = baiterekMessagePEPOut.o_uuid,
                        senderOrg = Const_esedo.baiterek_guid,
                        fromSpecified = true,
                        from = Const_esedo.baiterek_id,
                        href = baiterekMessagePEPOut.o_uuid,
                        performers = new long?[] { baiterekMessagePEPOut.o_delivery_org_esedo }, // , 1629005
                        senderOrgSpecified = true
                    },
                    appendCount = baiterekMessagePEPOut.o_in_amount,
                    address = baiterekMessagePEPOut.o_address,
                    citizenship = baiterekMessagePEPOut.o_rezidency,
                    carrierType = "1",
                    character = baiterekMessagePEPOut.o_character,
                    characterSpecified = true,
                    controlTypeOuterCode = "",
                    controlTypeOuterNameKz = "",
                    controlTypeOuterNameRu = "",
                    deliveryDate = Convert.ToDateTime(baiterekMessagePEPOut.o_delivery_date),
                    deliveryDateSpecified = true,
                    description = baiterekMessagePEPOut.o_description,
                    docDateR = Convert.ToDateTime(baiterekMessagePEPOut.o_doc_dateR),
                    docDateRSpecified = true,
                    docNoR = baiterekMessagePEPOut.o_doc_NoR,
                    docKind = baiterekMessagePEPOut.o_doc_kind,
                    docKindSpecified = true,
                    docLang = baiterekMessagePEPOut.o_doc_lang,
                    docReqAuthor = baiterekMessagePEPOut.o_author_name,
                    documentType = baiterekMessagePEPOut.o_doc_type,
                    documentTypeSpecified = true,
                    email = baiterekMessagePEPOut.o_email,
                    executionDate = baiterekMessagePEPOut.o_date_end == string.Empty ? Convert.ToDateTime(baiterekMessagePEPOut.o_date_end) : DateTime.Now.AddDays(30),
                    juridicallyName = baiterekMessagePEPOut.o_legal,
                    idPortalInternalSpecified = true,
                    idPortalInternal = long.Parse(baiterekMessagePEPOut.o_id_portal),
                    middlename = baiterekMessagePEPOut.o_middlename,
                    name = baiterekMessagePEPOut.o_firstname,
                    surname = baiterekMessagePEPOut.o_surname,
                    note = baiterekMessagePEPOut.o_description,
                    phone = baiterekMessagePEPOut.o_contact_phone,
                    portalSign = baiterekMessagePEPOut.o_portal_sign,
                    secondSignData = baiterekMessagePEPOut.o_canc_sign,
                    secondSignEnabled = "1",
                    preparedDate = Convert.ToDateTime(baiterekMessagePEPOut.o_prepared_date),
                    preparedDateSpecified = true,
                    regDateOL = baiterekMessagePEPOut.o_date_out != string.Empty ? Convert.ToDateTime(baiterekMessagePEPOut.o_date_out) : DateTime.Now,
                    regDateOLSpecified = baiterekMessagePEPOut.o_date_out != string.Empty ? true : false,
                    regNumberOL = baiterekMessagePEPOut.o_doc_number,
                    region = "",
                    sheetCount = "0",
                    signerNameKz = baiterekMessagePEPOut.o_signatory,
                    signerNameRu = baiterekMessagePEPOut.o_signatory,
                    userUin = baiterekMessagePEPOut.o_useruin,
                    executionDateSpecified = false
                };
                Const_esedo.QGClient qGClient = Const_esedo.qGClients.FirstOrDefault(p => p.ID == baiterekMessagePEPOut.o_delivery_org_esedo);
                DocSender.DocumentRequest documentRequest = new DocSender.DocumentRequest()
                {
                    requestDate = DateTime.Now,
                    sender = Const_esedo.sender,
                    receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                    data = docOL
                };
                response_to_org = documentPortClient.getDocument(documentRequest);
            }
            catch (Exception ex)
            {
                response_to_org = new DocSender.DocumentResponse();
                response_to_org.responseDate = DateTime.Now;
                response_to_org.data_ = new DocSender.qujatGatewayDelivered()
                {
                    date = DateTime.Now,
                    status = 0,
                    error = ex.Message,
                    metadataSystem = new DocSender.metadataSystem()
                    {
                        activityId = baiterekMessagePEPOut.o_uuid,
                        from = Const_esedo.qujatgateway_id,
                        fromSpecified = true,
                        href = baiterekMessagePEPOut.o_uuid,
                        performers = new long?[] { Const_esedo.baiterek_id },
                        senderOrg = Const_esedo.qujatgateway_guid
                    }
                };
            }
            var serializer1 = new XmlSerializer(response_to_org.GetType());
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer1.Serialize(writer, response_to_org);
            }
            string tmp = sb.ToString();
            return response_to_org;
        }
        public string downloadFromESEDOFiles(BaiterekMessageFilesRequest baiterekMessageFilesRequest)
        {
            string error = "";
            
            if (baiterekMessageFilesRequest.files == null)
                error += "No files\n";
            else
            {
                string[] fileIDs = new string[baiterekMessageFilesRequest.files.Length];
                int i = 0;
                foreach (var id in baiterekMessageFilesRequest.files)
                {
                    if (id.id == null || id.id == "")
                    {
                        error += "No file ID\n";
                    }
                    else
                    {
                        fileIDs[i++] = id.id;
                    }
                }
                if (error == "")
                {
                    DoDownloadAsync(baiterekMessageFilesRequest.id, fileIDs);
                }
            }
            if (error != "")
                return error;
            else
            {
                return "Ok";
            }
        }
        public void SendToESEDOFiles(BaiterekMessageFiles baiterekMessageFiles)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            
            DoUploadAsync(baiterekMessageFiles);
        }
        public string checkESEDOFiles(BaiterekMessageFilesRequest baiterekMessageFilesRequest)
        {
            string result = "Ok";
            string[] fileIDs = new string[baiterekMessageFilesRequest.files.Length];
            int i = 0;
            foreach (var id in baiterekMessageFilesRequest.files)
            {
                if (id.id == null || id.id == "")
                {
                    result += "No file ID\n";
                }
                else
                {
                    fileIDs[i++] = id.id;
                }
            }
            if (result == "Ok")
            {

                ProcessorPortClient portClient = new ProcessorPortClient();
                GetMessageRequest getMessage = new GetMessageRequest();
                TempStorageRequest tempStorage = new TempStorageRequest()
                {
                    credentials = new SenderInfo() { senderId = "qujatgateway", password = "b9m5mb7E" },
                    type = TempStorageRequestType.GET_FILE_INFO,
                    getFileInfoRequest = fileIDs,
                };
                SyncSendMessageRequest messageRequest = new SyncSendMessageRequest()
                {
                    requestInfo = new SyncMessageInfo()
                    {
                        messageId = "",//"352cca73-621e-4d63-bcad-bb28d32a06a2",
                        messageDate = DateTime.Now,
                        serviceId = "EDS_TEMP_FILES",
                        sender = new IntegrationDoc.HedReference.SenderInfo() { senderId = "baiterek", password = "baiterek" },
                        sessionId = ""//"eb77ee5b-df27-4c34-bd0f-b48c90f0b154"
                    },
                    requestData = new RequestData() { data_ = new Trans() { tempStorageRequest = tempStorage } }
                };
                getMessage.request = messageRequest;
                try
                {
                    GetMessageResponse getMessageResponse = portClient.GetMessage(getMessage);
                    if (getMessageResponse.response.responseInfo.status.code != "OK")
                    {
                        throw new Exception("HED status  - responded with fail");
                    }
                    FileInfoResult[] fileInfoResults = ((Trans2)getMessageResponse.response.responseData.data).tempStorageResponse.getFileInfoResponse.fileInfoResults;
                    foreach (var file in fileInfoResults)
                    {
                        if(file.status.status!= "Success")
                        {
                            result += String.Format("File with ID: {0}\n", file.fileIdentifier);
                        }
                        else if(file.meta==null && file.meta.deleted)
                        {
                            result += String.Format($"File with ID {file.fileIdentifier} is not accessible\n");
                        }
                    }
                }
                catch(Exception ex)
                {
                    result += ex.Message;
                    return result;
                }
            }
            return result;
        }
        public async Task DoUploadAsync(BaiterekMessageFiles baiterekMessageFiles)
        {
            BaiterekMessageResponse resp = await Task.Run(() => Upload(baiterekMessageFiles));
            string res = resp.error=="1"? "error": makeRequestFileListXML(resp);
        }
        public BaiterekMessageResponse Upload(BaiterekMessageFiles baiterekMessageFiles)
        {
            UploadFileRequest[] uploads = new UploadFileRequest[baiterekMessageFiles.files.Length];
            for (int i = 0; i < baiterekMessageFiles.files.Length; i++)
            {
                UploadFileRequest upload = new UploadFileRequest
                {
                    content = System.Convert.FromBase64String(baiterekMessageFiles.files[i].data),
                    fileProcessIdentifier = baiterekMessageFiles.id,
                    lifeTime = 2592000000, //86400000
                    lifeTimeSpecified = true,
                    name = baiterekMessageFiles.files[i].name,
                    needToBeConfirmedSpecified = false,
                    mime = "application/octet-stream"
                };
                uploads[i] = upload;
            }
            GetMessageResponse hedResponce;
            BaiterekMessageResponse baiterekMessageResponse = new BaiterekMessageResponse() { error = "0", o_unid = baiterekMessageFiles.id, redirection_specified = baiterekMessageFiles.redirection_specified };
            try
            {
                const int high = 7500000; //верхний порог размера файлов, а также максимальный размер пула
                List<(int a, int b)> tuppleList = new List<(int, int)>(); // список кортежей из пары значений (размер файла, индекс в upload)
                List<List<int>> group = new List<List<int>>(); // список списков разбиения  файлов по суммарному размеру <=high
                int total_volume = 0;
                for (int i = 0; i < uploads.Length; i++) // Забивка списка кортежей(размер, индекс) и проверка файлов по ограничителю 
                {
                    if (uploads[i].content.Length > high)
                        throw new Exception($"Превышен максимальный лимит {high} byte для файла {uploads[i].name} с размером {uploads[i].content.Length} byte");
                    total_volume += uploads[i].content.Length;
                    tuppleList.Add((uploads[i].content.Length, i));
                }
                tuppleList.Sort();
                tuppleList.Reverse();
                while (tuppleList.Count > 0)
                {
                    int volume = (int)high, i = 0;
                    List<int> tempList = new List<int>();
                    tempList.Add(tuppleList[0].b);
                    volume -= tuppleList[0].a;
                    tuppleList.RemoveAt(0);
                    while ((tuppleList.Where(p => p.a <= volume).Count() != 0) && (i < tuppleList.Count))
                    {
                        if (tuppleList[i].a <= volume)
                        {
                            tempList.Add(tuppleList[i].b);
                            volume -= tuppleList[i].a;
                            tuppleList.RemoveAt(i);
                        }
                        else { i++; }
                    }
                    group.Add(tempList);
                }
                DocSender.attachment[] attachments_ = new DocSender.attachment[uploads.Length];
                attach[] id = new attach[uploads.Length];
                int z = 0;
                foreach (var q in group)
                {
                    UploadFileRequest[] Sub_uploads = new UploadFileRequest[q.Count];
                    for (int i = 0; i < q.Count; i++)
                        Sub_uploads[i] = uploads[q[i]];
                    hedResponce = sendToHedFiles(Sub_uploads);
                    //JavaScriptSerializer serializer = new JavaScriptSerializer();
                    //dynamic a = hedResponce.response.responseData.data;
                    //string json2 = serializer.Serialize(hedResponce);
                    string json = JsonConvert.SerializeObject(hedResponce);
                    GetMessageResponse hedResponce1 = JsonConvert.DeserializeObject<GetMessageResponse>(json);
                    FileUploadResult[] fileUploadResults = hedResponce.response.responseData.data_.tempStorageResponse.uploadResponse.uploadFileResults;

                    for (int i = 0; (i < fileUploadResults.Length) && (z < uploads.Length); i++, z++)
                    {
                        id[z] = new attach() { fileId = fileUploadResults[i].fileIdentifier };
                        
                    }
                }
                baiterekMessageResponse.file_id = id;
                //throw new Exception("dfsdf");
            }
            catch (Exception ex)
            {
                baiterekMessageResponse.error = "1";
            }
            return baiterekMessageResponse;
        }
        public GetMessageResponse sendToHedFiles(UploadFileRequest[] uploadFileRequest) //try_catch
        {
            ProcessorPortClient portClient = new ProcessorPortClient();
            SyncSendMessageRequest syncSendMessageRequest = new SyncSendMessageRequest()
            {
                requestInfo = new SyncMessageInfo()
                {
                    messageId = "",//"352cca73-621e-4d63-bcad-bb28d32a06a2",
                    messageDate = DateTime.Now,
                    serviceId = "EDS_TEMP_FILES",
                    sender = new IntegrationDoc.HedReference.SenderInfo() { senderId = "baiterek", password = "baiterek" },
                    sessionId = "",//"eb77ee5b-df27-4c34-bd0f-b48c90f0b154"
                },
                requestData = new RequestData()
                {
                    data_ = new Trans
                    {
                        tempStorageRequest = new TempStorageRequest
                        {
                            credentials = new SenderInfo() { senderId = "qujatgateway", password = "b9m5mb7E" },
                            type = TempStorageRequestType.UPLOAD,
                            uploadRequest = uploadFileRequest,
                        }
                    }
                }
            };
            GetMessageRequest getMessageRequest = new GetMessageRequest()
            {
                request = syncSendMessageRequest
            };
            #region Serialization
            //XmlSerializer formatter = new XmlSerializer(typeof(GetMessageRequest), new Type[] { typeof(SenderInfo), typeof(TempStorageRequestType), typeof(TempStorageRequest), typeof(HedReference.SenderInfo), typeof(SyncMessageInfo), typeof(RequestData), typeof(Trans) });
            //using (FileStream fs = new FileStream("send_files.xml", FileMode.Create, FileAccess.ReadWrite))
            //{
            //    formatter.Serialize(fs, getMessageRequest);
            //    Console.WriteLine("Объект сериализован");
            //}
            //using (FileStream fs = new FileStream("send_files.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            //    GetMessageRequest newreq = (GetMessageRequest)formatter.Deserialize(fs);
            //}
            #endregion
            GetMessageResponse response = new GetMessageResponse();
            try
            {
                response = portClient.GetMessage(getMessageRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error on HED (" + ex.Message + ")");
            }
            return response;
        }
        public GetMessageResponse[] downloadFromHedFiles(string[] fileIdentifiers) //try_catch
        {
            ProcessorPortClient portClient = new ProcessorPortClient();
            GetMessageRequest getMessage = new GetMessageRequest();
            TempStorageRequest tempStorage = new TempStorageRequest()
            {
                credentials = new SenderInfo() { senderId = "qujatgateway", password = "b9m5mb7E" },
                type = TempStorageRequestType.GET_FILE_INFO,
                getFileInfoRequest = fileIdentifiers,
            };
            SyncSendMessageRequest messageRequest = new SyncSendMessageRequest()
            {
                requestInfo = new SyncMessageInfo()
                {
                    messageId = "",//"352cca73-621e-4d63-bcad-bb28d32a06a2",
                    messageDate = DateTime.Now,
                    serviceId = "EDS_TEMP_FILES",
                    sender = new IntegrationDoc.HedReference.SenderInfo() { senderId = "baiterek", password = "baiterek" },
                    sessionId = ""//"eb77ee5b-df27-4c34-bd0f-b48c90f0b154"
                },
                requestData = new RequestData() { data_ = new Trans() { tempStorageRequest = tempStorage } }
            };
            getMessage.request = messageRequest;
            GetMessageResponse getMessageResponse = portClient.GetMessage(getMessage);
            if (getMessageResponse.response.responseInfo.status.code != "OK")
            {
                throw new Exception("HED status responded  - not OK");
            }
            long high = Const_esedo.high; //верхний порог размера файлов, а также максимальный размер пула
            List<(long a, string b)> tuppleList = new List<(long, string)>(); // список кортежей из пары значений (размер файла, id файлов)
            List<List<string>> group = new List<List<string>>(); // список списков разбиения  файлов по суммарному размеру <=high
            long total_volume = 0;
            FileInfoResult[] fileInfoResults = ((Trans2)getMessageResponse.response.responseData.data).tempStorageResponse.getFileInfoResponse.fileInfoResults;
            for (int i = 0; i < fileInfoResults.Length; i++) // Забивка списка кортежей(размер, id) и проверка файлов по ограничителю 
            {
                if (fileInfoResults[i].meta.size > (high)*2)
                    throw new Exception($"Превышен максимальный лимит в 2 раза {high} byte для файла {fileInfoResults[i].meta.name} с размером {fileInfoResults[i].meta.size} byte");
                total_volume += fileInfoResults[i].meta.size;

                tuppleList.Add((fileInfoResults[i].meta.size, fileInfoResults[i].fileIdentifier));
            }
            tuppleList.Sort();
            tuppleList.Reverse();
            while (tuppleList.Count > 0)
            {
                long volume = high;
                
                List<string> tempList = new List<string>();
                tempList.Add(tuppleList[0].b);
                volume -= tuppleList[0].a;
                tuppleList.RemoveAt(0);
                int i = 0;
                while ((tuppleList.Where(p => p.a <= volume).Count() != 0) && (i < tuppleList.Count))
                {
                    if (tuppleList[i].a <= volume)
                    {
                        tempList.Add(tuppleList[i].b);
                        volume -= tuppleList[i].a;
                        tuppleList.RemoveAt(i);
                    }
                    else { i++; }
                }
                group.Add(tempList);
            }

            GetMessageResponse[] response = new GetMessageResponse[group.Count];
            GetMessageRequest getMessageRequest = new GetMessageRequest();
            
            int clocker = 0, counter_of_group=0;
            bool error = false;
            foreach( var set in group)
            {
                string[] tmpFileCache = new string[set.Count];
                int counter_in_group = 0;
                foreach(var id in set)
                {
                    tmpFileCache[counter_in_group++] = id;
                }
                TempStorageRequest tempStorageRequest = new TempStorageRequest
                {
                    credentials = new SenderInfo() { senderId = "qujatgateway", password = "b9m5mb7E" },
                    type = TempStorageRequestType.DOWNLOAD,
                    downloadRequest = tmpFileCache
                };
                
                SyncSendMessageRequest syncSendMessageRequest = new SyncSendMessageRequest()
                {
                    requestInfo = new SyncMessageInfo()
                    {
                        messageId = "",//"352cca73-621e-4d63-bcad-bb28d32a06a2",
                        messageDate = DateTime.Now,
                        serviceId = "EDS_TEMP_FILES",
                        sender = new IntegrationDoc.HedReference.SenderInfo() { senderId = "baiterek", password = "baiterek" },
                        sessionId = ""//"eb77ee5b-df27-4c34-bd0f-b48c90f0b154"
                    },
                    requestData = new RequestData() { data_ = new Trans() { tempStorageRequest = tempStorageRequest } }
                };
                getMessageRequest.request = syncSendMessageRequest;

                #region Serialization
                //XmlSerializer formatter = new XmlSerializer(typeof(GetMessageRequest), new Type[] { typeof(SenderInfo), typeof(TempStorageRequestType), typeof(TempStorageRequest), typeof(HedReference.SenderInfo), typeof(SyncMessageInfo), typeof(RequestData), typeof(Trans) });
                //using (FileStream fs = new FileStream("download_files.xml", FileMode.Create, FileAccess.ReadWrite))
                //{
                //    formatter.Serialize(fs, getMessageRequest);
                //}
                //using (FileStream fs = new FileStream("download_files.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                //{
                //    GetMessageRequest newreq = (GetMessageRequest)formatter.Deserialize(fs);
                //}
                #endregion
                do
                {
                    try
                    {
                        response[counter_of_group++] = portClient.GetMessage(getMessageRequest);
                        error = false;
                        clocker = 0;
                    }
                    catch (Exception ex)
                    {
                        error = true;
                        
                        Thread.Sleep(TimeSpan.FromSeconds(clocker * clocker * 10));
                        if (clocker++ >= 3)
                        {
                            using (StreamWriter writer = new StreamWriter(path: @"C:\connector_logs\conn_hed.txt", append: true))
                            {
                                writer.WriteLine(DateTime.Now);
                                for (int w = 0; w < tmpFileCache.Length; w++)
                                {
                                    writer.WriteLine("\t"+ex.Message + "\n");
                                    writer.WriteLine("\t"+tmpFileCache[w]);
                                }
                            }
                            throw new Exception(DateTime.Now+": error on read from hed ");
                        }
                    }
                } while (error);
            }
            return response;
        }
        public qujatGatewayDelivered SendUniversalMessage(DocumentRequest request)
        {
            string activity = "";
            int status_ = 1;
            string error_text = "";
            Const_esedo.QGClient qGClient = Const_esedo.qGClients.FirstOrDefault(p => p.ID == request.data.metadataSystem.from);
            Sbapi sbapi = new Sbapi();
            try
            {
                switch (request.data)
                {
                    case DocReceiver.QGDocument q:
                        QGProcessingNew(q, ref sbapi, ref activity);
                        DocSender.DocumentPortClient documentPort = new DocSender.DocumentPortClient();
                        DocSender.DocumentRequest documentRequest = new DocSender.DocumentRequest()
                        {
                            requestDate = DateTime.Now,
                            sender = Const_esedo.sender,
                            receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                            data = new DocSender.stateDelivered()
                            {
                                dateSpecified = true,
                                date = Convert.ToDateTime(sbapi.header.message.created),
                                metadataSystem = new DocSender.metadataSystem()
                                {
                                    activityId = request.data.metadataSystem.href,
                                    fromSpecified = true,
                                    from = Const_esedo.baiterek_id,
                                    senderOrgSpecified = true,
                                    senderOrg = Const_esedo.baiterek_guid,
                                    href = request.data.metadataSystem.href,//sbapi.body.object_.text,
                                    performers = new long?[] { request.data.metadataSystem.from }
                                }
                            }
                        };
                        var serializer1 = new XmlSerializer(documentRequest.GetType(), new Type[] { typeof(DocSender.metadataSystem), typeof(DocSender.stateDelivered)});
                        var sb = new StringBuilder();
                        using (TextWriter writer = new StringWriter(sb))
                        {
                            serializer1.Serialize(writer, documentRequest);
                        }
                        string tmp = sb.ToString();
                        documentPort.getDocument(documentRequest);
                        break;
                    case DocReceiver.docAppeal doc:
                        AppealProcessing(doc, ref sbapi, ref activity);
                        DocSender.DocumentPortClient documentPort1 = new DocSender.DocumentPortClient();
                        DocSender.DocumentRequest documentRequest1 = new DocSender.DocumentRequest()
                        {
                            requestDate = DateTime.Now,
                            sender = Const_esedo.sender,
                            receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                            data = new DocSender.stateDelivered()
                            {
                                dateSpecified = true,
                                date = Convert.ToDateTime(sbapi.header.message.created),
                                metadataSystem = new DocSender.metadataSystem()
                                {
                                    activityId = request.data.metadataSystem.href,
                                    fromSpecified = true,
                                    from = Const_esedo.baiterek_id,
                                    senderOrgSpecified = true,
                                    senderOrg = Const_esedo.baiterek_guid,
                                    href = request.data.metadataSystem.href,
                                    performers = new long?[] { request.data.metadataSystem.from }
                                },
                                idPortalInternal = doc.idPortalInternal,
                                idPortalInternalSpecified = true,
                                userUin = doc.userUin
                            }
                        };
                        var serializer2 = new XmlSerializer(documentRequest1.GetType(), new Type[] { typeof(DocSender.metadataSystem), typeof(DocSender.stateDelivered) });
                        var sb1 = new StringBuilder();
                        using (TextWriter writer = new StringWriter(sb1))
                        {
                            serializer2.Serialize(writer, documentRequest1);
                        }
                        string tmp2 = sb1.ToString();
                        documentPort1.getDocument(documentRequest1);
                        break;
                    case DocReceiver.docOL doc_ol:
                        DocOlProcessing(doc_ol, ref sbapi, ref activity);
                        DocSender.DocumentPortClient documentPort2 = new DocSender.DocumentPortClient();
                        DocSender.DocumentRequest documentRequest2 = new DocSender.DocumentRequest()
                        {
                            requestDate = DateTime.Now,
                            sender = Const_esedo.sender,
                            receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                            data = new DocSender.stateDelivered()
                            {
                                dateSpecified = true,
                                date = Convert.ToDateTime(sbapi.header.message.created),
                                metadataSystem = new DocSender.metadataSystem()
                                {
                                    activityId = request.data.metadataSystem.href,
                                    fromSpecified = true,
                                    from = Const_esedo.baiterek_id,
                                    senderOrgSpecified = true,
                                    senderOrg = Const_esedo.baiterek_guid,
                                    href = request.data.metadataSystem.href,
                                    performers = new long?[] { request.data.metadataSystem.from }
                                },
                                idPortalInternal = doc_ol.idPortalInternal,
                                idPortalInternalSpecified = true,
                                userUin = doc_ol.userUin
                            }
                        };
                        if (doc_ol.idPortalInternal != 0)
                        {
                            DocSender.DocumentRequest documentRequest2_PEP = new DocSender.DocumentRequest()
                            {
                                requestDate = DateTime.Now,
                                sender = Const_esedo.sender,
                                receiver = new string[] { qGClient != null ? qGClient.receiver_code : Const_esedo.receiver_esedo },
                                data = new DocSender.stateDelivered()
                                {
                                    dateSpecified = true,
                                    date = Convert.ToDateTime(sbapi.header.message.created),
                                    metadataSystem = new DocSender.metadataSystem()
                                    {
                                        activityId = request.data.metadataSystem.href,
                                        fromSpecified = true,
                                        from = Const_esedo.baiterek_id,
                                        senderOrgSpecified = true,
                                        senderOrg = Const_esedo.baiterek_guid,
                                        href = request.data.metadataSystem.href,
                                        performers = new long?[] { doc_ol.idPortalInternal.ToString().Length < 7 ? 17473609 : 4842363 }
                                    },
                                    idPortalInternal = doc_ol.idPortalInternal,
                                    idPortalInternalSpecified = true,
                                    userUin = doc_ol.userUin
                                }
                            };
                            documentPort2.getDocument(documentRequest2_PEP);
                        }
                        //var serializer3 = new XmlSerializer(documentRequest2.GetType(), new Type[] { typeof(DocSender.metadataSystem), typeof(DocSender.stateDelivered) });
                        //var sb2 = new StringBuilder();
                        //using (TextWriter writer = new StringWriter(sb2))
                        //{
                        //    serializer3.Serialize(writer, documentRequest2);
                        //}
                        //string tmp3 = sb2.ToString();
                        documentPort2.getDocument(documentRequest2);

                        //((DocSender.stateDelivered)documentRequest2.data).metadataSystem.activityId = request.data.metadataSystem.href + "_";
                        
                        break;
                    case DocReceiver.stateDelivered statedeliv:
                        StateDeliveryProcessing(statedeliv, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateRegistered stateReg:
                        StateRegisteredProcessing(stateReg, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateNotValid stateNotValid:
                        StateNotValidProcessing(stateNotValid, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateExecution state_execution:
                        StateExecutionProcessing(state_execution, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateFinished state_finished:
                        StateFinishedProcessing(state_finished, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateNewControl state_new_control:
                        StateNewControlProcessing(state_new_control, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateProlongExDate state_prolong_ex_date:
                        StateProlongExDateProcessing(state_prolong_ex_date, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateNewExDate state_new_ex_date:
                        StateNewExDateProcessing(state_new_ex_date, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateTakeOfControl state_take_of_control:
                        StateTakeOfControl(state_take_of_control, ref sbapi, ref activity);
                        break;
                    case DocReceiver.stateStartProcessResponse state_start_process_response:
                        stateStartProcessResponseProcessing(state_start_process_response, ref sbapi, ref activity);
                        break;
                    case DocReceiver.docSection doc_section:
                        DocSectionProcessing(doc_section, ref sbapi, ref activity);
                        break;
                }
            }
            catch (Exception ex)
            {
                status_ = 0;
                error_text = ex.Message + ":" + ex.StackTrace;
            }
            //QGDocument qG = (DocReceiver.QGDocument)(request.data);
            // Проверка на наличие среди перформеров Байтерека

            //StringReader stringReader = new StringReader(response);
            //XDocument xDocument = new XDocument();
            //xDocument = XDocument.Load(stringReader);
            //XAttribute attribute = xDocument.Element("sbapi").Element("header").Element("error").Attribute("id");
            //if (attribute.Value == "0")
            //    status_=1;
            qujatGatewayDelivered delivered = new qujatGatewayDelivered()
            {
                status = status_,
                date = DateTime.Now,
                error = error_text,
                metadataSystem = new metadataSystem()
                {
                    activityId = activity,
                    senderOrgSpecified = true,
                    senderOrg = Const_esedo.baiterek_guid,
                    fromSpecified = true,
                    from = Const_esedo.baiterek_id,
                    href = (status_ == 1) ? sbapi.body.object_.text : "",
                    performers = new ArrayOfNullableOfInt64 { request.data.metadataSystem.from }
                }
            };
            return delivered;
        }
        public void DocSectionProcessing(docSection dS, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in dS.metadataSystem.performers)
            { if (t == Const_esedo.baiterek_id) { error = false; } }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            
            string error_message = "";
            try
            {
                BaiterekMessageIn baiterekMessage = new BaiterekMessageIn()
                {
                    o_correspondent = (int)dS.metadataSystem.from,
                    o_date_end = dS.executionDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    o_date_event = dS.docDate.AddHours(6).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    o_description = dS.description,
                    o_doc_kind = dS.docKind,
                    o_language = "KZRU",
                    o_character = dS.character,
                    o_outgoing_number = dS.docNumber.Trim(new char[] { '-' }),
                    o_phone = dS.employeePhone.Trim(new char[] { '-' }),
                    o_signatory = dS.signerNameRu ,
                    o_user_corr = dS.executor,
                    o_unid = dS.metadataSystem.href,
                    o_control_type_outer_code = dS.controlTypeCode,
                    o_control_type_outer_name = dS.controlTypeNameKz+"/"+dS.controlTypeNameRu,
                    o_author_name = dS.signerNameRu,
                    o_docSection = dS.docSection1,
                    o_docSectionId = dS.documentSectionId,
                    o_prepared_date = dS.preparedDate.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
                
                
                string response;
                try
                {
                    response = makeRequestDocXML(baiterekMessage);
                    XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                    using (StringReader fs = new StringReader(response))
                    {
                        sbapi = (Sbapi)formatter.Deserialize(fs);
                    }
                    //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                    if (sbapi.header.error.id != "0")
                    {
                        error = true;
                        error_message = "error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text;
                    }
                    else if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                    {
                        error = true;
                        error_message = sbapi.body.object_.text;
                    }
                    if (error)
                    { throw new Exception(error_message); }
                }
                catch (Exception ex)
                {
                    error_message ="error on call to REST(POST) of Simbase: " + ex.Message;
                    throw new Exception(error_message);
                }

                activity = dS.metadataSystem.href;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void QGProcessingNew(QGDocument qG, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in qG.metadataSystem.performers)
            { if (t == Const_esedo.baiterek_id) { error = false; } }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            int Length = 0;
            if (qG.attachments != null)
            {
                Length = qG.attachments.Length;
            }
            string[] fileIDs = new string[Length];
            int i = 0;
            if (qG.attachments != null)
            {
                foreach (var attach in qG.attachments)
                    fileIDs[i++] = attach.fileIdentifier;
            }
            string error_message = "";
            try
            {
                BaiterekMessageInNew baiterekMessage = new BaiterekMessageInNew()
                {
                    o_correspondent = (int)qG.metadataSystem.from,
                    o_date_end = qG.executionDate == new DateTime(0001, 01, 01, 00, 00, 00) ? "" : qG.executionDate.ToString("s"),
                    o_doc_date = qG.docDate.AddHours(6).ToString("s"),
                    o_description = qG.description,
                    o_doc_kind = qG.docKind,
                    o_language = qG.docLang,
                    o_outgoing_number = qG.docNo.TrimStart(new char[] { '-' }).Trim(new char[] { '\n' }),
                    o_phone = qG.employeePhone.TrimStart(new char[] { '-' }).Trim(new char[] { '\n' }),
                    o_signatory = (qG.signerNameKz != "") ? qG.signerNameKz : qG.signerNameRu,
                    o_user_corr = qG.executor,
                    o_unid = qG.metadataSystem.href,
                    o_control_type_outer_code = qG.controlTypeOuterCode,
                    o_control_type_outer_name = qG.controlTypeOuterNameRu,
                    o_author_name = (qG.authorNameRu == "") ? qG.authorNameKz : qG.authorNameRu,
                    o_doc_to_number = qG.docToNumber,
                    o_out_time = qG.outTime == new DateTime(0001, 01, 01, 00, 00, 00) ? "" : qG.outTime.ToString("s"),
                    o_prepared_date = qG.preparedDate == new DateTime(0001, 01, 01, 00, 00, 00) ? "" : qG.preparedDate.ToString("s"),
                    o_character = qG.character,
                    files = fileIDs,
                    o_canc_sign = qG.secondSignData,
                    o_doc_rec_post = (qG.docRecPostKz == "") ? qG.docRecPostRu?.Trim() : qG.docRecPostKz?.Trim(),
                    o_document_receiver = (qG.documentReceiverKz == "") ? qG.documentReceiverRu : qG.documentReceiverKz,
                    o_resolution = qG.resolutionText,
                    o_doc_page_num = qG.sheetCount,
                    o_in_amount = qG.appendCount,
                    
                    
                };
                string response;
                try
                {
                    response = makeRequestNewXML(baiterekMessage);
                    XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                    using (StringReader fs = new StringReader(response))
                    {
                        sbapi = (Sbapi)formatter.Deserialize(fs);
                    }
                    //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                    if (sbapi.header.error.id != "0")
                    {
                        error = true;
                        error_message = "error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text;
                    }
                    else if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                    {
                        error = true;
                        error_message = sbapi.body.object_.text;
                    }
                    if (error)
                    { throw new Exception(error_message); }
                }
                catch (Exception ex)
                {
                    error_message = "error on call to REST(POST) of Simbase: "+ ex.Message;
                    throw new Exception(error_message);
                }

                activity = qG.metadataSystem.href;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (qG.attachments != null)
            {
                DoDownloadAsync(sbapi.body.object_.id, fileIDs);
            }
        }
        public async Task DoDownloadAsync(string id, string[] file_ids)
        {
            try
            {
                await Task.Run(() => DoDownload(id, file_ids));
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }
        public void DoDownload (string id, string[] file_ids)
        {
            bool error = false;
            string error_message = "";
            GetMessageResponse[] getMessageResponse;
            try
            {
                try
                {
                    getMessageResponse = downloadFromHedFiles(file_ids);
                }
                catch (Exception ex)
                {
                    throw new Exceptions_ESEDO_Receive(ex.Message);
                }

                BaiterekMessageFiles baiterekMessageFiles = new BaiterekMessageFiles() { id = id };
                try
                {
                    for (int r = 0; r < getMessageResponse.Length; r++)
                    {
                        TempStorageResponse temp = getMessageResponse[r].response.responseData.data_.tempStorageResponse;
                        if (temp.downloadResponse != null)
                        {
                            if (temp.downloadResponse.status.status == "Has errors")
                            {
                                error_message += (temp.downloadResponse.status.message + "!");
                                error = true;
                            }
                        }
                        int i = 0;
                        baiterekMessageFiles.files = new BaiterekMessageFiles.Files[temp.downloadResponse.fileDownloadResults.Length];
                        foreach (var j in temp.downloadResponse.fileDownloadResults)
                        {
                            if (j.content == null)
                            {
                                error = true;
                                error_message += $"Bad file ({j.meta.name}), null content!";
                            }
                            else
                            {
                                BaiterekMessageFiles.Files file = new BaiterekMessageFiles.Files();
                                file.name = j.meta.name;
                                file.data = Convert.ToBase64String(j.content);
                                baiterekMessageFiles.files[i++] = file;
                            }
                        }
                        int counter = 1;
                        do
                        {
                            string response = string.Empty;
                            try
                            {
                                response = makeRequestFilesXML(baiterekMessageFiles);
                                Sbapi sbapi;
                                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                                using (StringReader fs = new StringReader(response))
                                {
                                    sbapi = (Sbapi)formatter.Deserialize(fs);
                                }
                                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                                if (sbapi.header.error.id != "0")
                                {
                                    error = true;
                                    error_message = "error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text;
                                    throw new Exception(error_message);
                                }
                                else
                                {
                                    error = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                error = true;

                                Thread.Sleep(TimeSpan.FromSeconds(counter * counter * 10));
                                if (counter++ >= 3)
                                {
                                    using (StreamWriter writer = new StreamWriter(path: @"C:\connector_logs\conn_simbase.txt", append: true))
                                    {
                                        writer.WriteLine(ex.Message);
                                        writer.WriteLine(response);
                                        error = false;
                                    }
                                    throw new Exception("error on read from hed ");
                                }
                            }
                        } while (error);
                    }
                    //Do_makeRequestFilesXMLAsync(baiterekMessageFiles);
                }
                catch (Exception ex)
                {
                    throw new Exceptions_ESEDO_Simbase(ex.Message);
                }
            }
            catch (Exceptions_ESEDO_Receive ex)
            {
                using (StreamWriter writer = new StreamWriter(path: @"C:\connector_logs\conn_receive.txt", append: true))
                {
                    writer.WriteLine(id);
                    for (int w = 0; w < file_ids.Length; w++)
                    {
                        writer.WriteLine(DateTime.Now);
                        writer.WriteLine(ex.Message);
                        writer.WriteLine("\t" + file_ids[w]);
                    }
                }
            }
            catch(Exceptions_ESEDO_Simbase ex)
            {
                using (StreamWriter writer = new StreamWriter(path: @"C:\connector_logs\conn_simbase.txt", append: true))
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
        public void AppealProcessing(docAppeal doc, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in doc.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            int Length = 0;
            if (doc.attachments != null)
            {
                Length = doc.attachments.Length;
            }
            string[] fileIDs = new string[Length];
            int i = 0;
            if (doc.attachments != null)
            {
                foreach (var attach in doc.attachments)
                    fileIDs[i++] = attach.fileIdentifier;
            }
            string error_message = "";
            try
            {
                BaiterekMessagePEP baiterekMessagePEP = new BaiterekMessagePEP()
                {
                    o_correspondent = doc.metadataSystem.senderOrg.ToString(),
                    o_address = doc.address,
                    o_contact_phone = doc.phone.TrimStart(new char[] { '-' }).Trim(new char[] { '\n' }),
                    o_rezidency = "KZ",
                    o_date_out = doc.outDate.ToString("s"),
                    o_author_name = doc.surname + " " + doc.name + " " + doc.middlename,
                    o_doc_lang = "KZRU",
                    o_doc_type = (int)doc.documentType,
                    o_email = doc.email,
                    o_surname = doc.surname,
                    o_firstname = doc.name,
                    o_middlename = doc.middlename,
                    o_useruin = doc.userUin,
                    o_in_amount = doc.attachments == null? "0": (doc.attachments.Length - 1).ToString(),
                    o_legal = (doc.juridicallyName == "") ? "\\" : doc.juridicallyName,
                    o_locality = Const_esedo.localityPEP,
                    o_id_portal = doc.idPortalInternal.ToString(),
                    o_delivery_date = doc.deliveryDate.ToString("s"),
                    o_type = Const_esedo.individualPEP,
                    o_unid = doc.metadataSystem.href,
                    o_doc_number = doc.outNumber.TrimStart(new char[] { '-' }).Trim(new char[] { '\n' }),
                    o_description = doc.description,
                    o_portal_sign = doc.sign,
                    files = fileIDs
                };
                //if (doc.sign != null)
                //{

                //    string xmlDoc = HttpUtility.HtmlDecode(doc.sign);
                //    StringReader stringReader = new StringReader(xmlDoc);
                //    XDocument xDocument = new XDocument();
                //    xDocument = XDocument.Load(stringReader);
                //    XNamespace xNamespace = "http://www.w3.org/2000/09/xmldsig#";
                //    baiterekMessagePEP.o_sign = xDocument.Root.Descendants(xNamespace + "X509Certificate").First().Value.Trim();
                //}
                //else
                //{
                //    error = true;
                //    error_message += "No sign data!";
                //}
                //if (error)
                //{
                //    throw new Exception(error_message);
                //}
                string response;
                try
                {
                    response = makeRequestXMLPEP(baiterekMessagePEP);
                    XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                    using (StringReader fs = new StringReader(response))
                    {
                        sbapi = (Sbapi)formatter.Deserialize(fs);
                    }
                    //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                    if (sbapi.header.error.id != "0")
                    {
                        error = true;
                        error_message = "error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text;
                    }
                    if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                    {
                        error = true;
                        error_message = sbapi.body.object_.text;
                    }
                    if (error)
                    { throw new Exception(error_message); }
                }
                catch (Exception ex)
                {
                    error_message = "error on call to REST(POST) of Simbase: " + ex.Message;
                    throw new Exception(error_message);
                }
                activity = doc.metadataSystem.href;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (doc.attachments != null)
            {
                DoDownloadAsync(sbapi.body.object_.id, fileIDs);
            }
        }
        public void DocOlProcessing(docOL doc, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in doc.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            int Length = 0;
            if (doc.attachments != null)
            {
                Length = doc.attachments.Length;
            }
            string[] fileIDs = new string[Length];
            int i = 0;
            if (doc.attachments != null)
            {
                foreach (var attach in doc.attachments)
                    fileIDs[i++] = attach.fileIdentifier;
            }
            string error_message = "";
            try
            {
                BaiterekMessagePEP orgMessagePEP = new BaiterekMessagePEP()
                {
                    o_correspondent = doc.metadataSystem.senderOrg.ToString(),
                    o_from = doc.metadataSystem.from.ToString(),
                    o_address = doc.address.TrimStart(new char[] { '-' }).Trim(new char[] { '\n' }),
                    o_contact_phone = doc.phone.TrimStart(new char[] { '-' }).Trim(new char[] { '\n' }),
                    o_character = doc.character,
                    o_rezidency = String.IsNullOrEmpty(doc.citizenship)? "Other":doc.citizenship,
                    o_date_out = doc.regDateOL.ToString("s"),
                    o_doc_kind = doc.docKind,
                    o_control_type_outer_code = doc.controlTypeOuterCode,
                    o_control_type_outer_name = doc.controlTypeOuterNameRu,
                    o_delivery_date = doc.deliveryDate.ToString("s"),
                    o_date_end = doc.executionDate.ToString("s"),
                    o_doc_lang = doc.docLang,
                    o_doc_type = doc.documentType,
                    o_email = doc.email,
                    o_surname = doc.surname,
                    o_firstname = doc.name,
                    o_middlename = doc.middlename,
                    o_useruin = doc.userUin,
                    o_legal = doc.juridicallyName,
                    o_locality = Const_esedo.localityPEP,
                    o_id_portal = doc.idPortalInternal.ToString(),
                    o_in_amount = doc.appendCount,
                    o_doc_dateR = doc.docDateR.ToString("s"),
                    o_doc_NoR = doc.docNoR.Trim(new char[] { '-' }),
                    o_type = Const_esedo.individualPEP,
                    o_unid = doc.metadataSystem.href,
                    o_doc_number = doc.regNumberOL,
                    o_description = doc.description + $"(текст обращения - {doc.note})",
                    o_signatory = (doc.signerNameKz != "") ? doc.signerNameKz : doc.signerNameRu,
                    o_author_name = doc.docReqAuthor,
                    o_canc_sign = doc.secondSignData,
                    o_portal_sign = doc.portalSign,
                    o_prepared_date = doc.preparedDate.ToString("s"),
                    files = fileIDs
                };
                //if (doc.portalSign != null)
                //{

                //    string xmlDoc = HttpUtility.HtmlDecode(doc.portalSign);
                //    StringReader stringReader = new StringReader(xmlDoc);
                //    XDocument xDocument = new XDocument();
                //    xDocument = XDocument.Load(stringReader);
                //    XNamespace xNamespace = "http://www.w3.org/2000/09/xmldsig#";
                //    orgMessagePEP.o_sign = xDocument.Root.Descendants(xNamespace + "X509Certificate").First().Value.Trim();
                //}
                //else
                //{
                //    error = true;
                //    error_message += "No sign data!";
                //}
                //if (error)
                //{
                //    throw new Exception(error_message);
                //}
                string response;
                try
                {
                    response = makeRequestXMLPEP(orgMessagePEP);
                    XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                    using (StringReader fs = new StringReader(response))
                    {
                        sbapi = (Sbapi)formatter.Deserialize(fs);
                    }
                    //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                    if (sbapi.header.error.id != "0")
                    {
                        error = true;
                        error_message = "error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text;
                    }
                    else if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                    {
                        error = true;
                        error_message = sbapi.body.object_.text;
                    }
                    if (error)
                    { throw new Exception(error_message); }
                }
                catch (Exception ex)
                {
                    error_message = "error on call to REST(POST) of Simbase: " + ex.Message;
                    throw new Exception(error_message);
                }
                activity = doc.metadataSystem.href;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (doc.attachments != null)
            {
                DoDownloadAsync(sbapi.body.object_.id, fileIDs);
            }
        }
        public void StateDeliveryProcessing(stateDelivered statedeliv, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in statedeliv.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = statedeliv.date.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = statedeliv.metadataSystem.href,
                o_from = statedeliv.metadataSystem.from.ToString(),
                o_status_delivery = "Доставлен до адресата, дата и время: " + gmt_corr.ToString("s"),
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateDelivered(" + statedeliv.metadataSystem.from.ToString()+", " + gmt_corr.ToString("s") + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " +sbapi.header.error.id+ " text = "+sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: "+ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void StateRegisteredProcessing(stateRegistered stateReg, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in stateReg.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = stateReg.date.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = stateReg.metadataSystem.href,
                o_from = stateReg.metadataSystem.from.ToString(),
                o_num_doc = stateReg.regNo.TrimStart(new char[] { '-' }).Trim(new char[] { '\n' }),
                o_date_reg_corr = gmt_corr.ToString("s"),
                o_status_register = "Зарегистрировано",
                o_canc_sign = stateReg.secondSignNotifData,
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateRegistered(" + stateReg.metadataSystem.from.ToString() + ", " + stateReg.regNo + "," + gmt_corr.ToString("s") + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void StateNotValidProcessing(stateNotValid stateNotValid, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in stateNotValid.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = stateNotValid.date.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = stateNotValid.metadataSystem.href,
                o_from = stateNotValid.metadataSystem.from.ToString(),
                o_status_register = "Не зарегистрирован из-за: " + stateNotValid.isValidReason,
                o_date_reg_corr = gmt_corr.ToString("s"),
                o_canc_sign = stateNotValid.secondSignNotifData,
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateNotValid(" + stateNotValid.metadataSystem.from.ToString() + ", " + stateNotValid.isValidReason + ", " + gmt_corr.ToString("s") + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void StateExecutionProcessing(stateExecution state_execution, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in state_execution.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = state_execution.execDate.AddHours(6);
            
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = state_execution.metadataSystem.href,
                o_from = state_execution.metadataSystem.from.ToString(),
                o_exec = state_execution.executive,
                o_exec_phone = state_execution.executivePhone.TrimStart(new char[] { '-' }).Trim(new char[] { '\n' }),
                o_date_exec = gmt_corr.ToString("s"),
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateExecution(" + state_execution.metadataSystem.from.ToString() + ", " + state_execution.executive + "," + state_execution.executivePhone + "," +
                gmt_corr.ToString("s") + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void StateFinishedProcessing(stateFinished state_finished, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in state_finished.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = state_finished.realDate.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = state_finished.metadataSystem.href,
                o_from = state_finished.metadataSystem.from.ToString(),
                o_author = state_finished.author,
                o_real_date = gmt_corr.ToString("s"),
                o_result_code = state_finished.resultCode,
                o_result_text = state_finished.resultText,
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateFinished(" + state_finished.metadataSystem.from.ToString() + ", " + state_finished.author + "," + gmt_corr.ToString("s") + "," +
                state_finished.resultCode + "," + state_finished.resultText + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void StateNewControlProcessing(stateNewControl state_new_control, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in state_new_control.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = state_new_control.execDate.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = state_new_control.metadataSystem.href,
                o_from = state_new_control.metadataSystem.from.ToString(),
                o_control_type = state_new_control.controlTypeCode,
                o_control_type_name = state_new_control.controlTypeNameRu,
                o_date_exec = gmt_corr.ToString("s"),
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateNewControl(" + state_new_control.metadataSystem.from.ToString() + ", " + state_new_control.controlTypeCode + "," + state_new_control.controlTypeNameRu + "," +
                gmt_corr.ToString("s") + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void StateProlongExDateProcessing(stateProlongExDate state_prolong_ex_date, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in state_prolong_ex_date.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = state_prolong_ex_date.execDate.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = state_prolong_ex_date.metadataSystem.href,
                o_from = state_prolong_ex_date.metadataSystem.from.ToString(),
                o_control_type = state_prolong_ex_date.controlTypeCode,
                o_control_type_name = state_prolong_ex_date.controlTypeNameRu,
                o_date_exec = gmt_corr.ToString("s"),
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateProlongExDate(" + state_prolong_ex_date.metadataSystem.from.ToString() + ", " + state_prolong_ex_date.controlTypeCode + "," + state_prolong_ex_date.controlTypeNameRu + "," +
                gmt_corr.ToString("s") + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void StateNewExDateProcessing(stateNewExDate state_new_ex_date, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in state_new_ex_date.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = state_new_ex_date.execDate==DateTime.MinValue?DateTime.MinValue:state_new_ex_date.execDate.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = state_new_ex_date.metadataSystem.href,
                o_from = state_new_ex_date.metadataSystem.from.ToString(),
                o_control_type = state_new_ex_date.controlTypeCode,
                o_control_type_name = state_new_ex_date.controlTypeNameRu,
                o_date_exec = state_new_ex_date.execDate == DateTime.MinValue?"":gmt_corr.ToString("s"),
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateNewExDate(" + state_new_ex_date.metadataSystem.from.ToString() + ", " + state_new_ex_date.controlTypeCode + "," + state_new_ex_date.controlTypeNameRu + "," +
                gmt_corr.ToString("s") + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void StateTakeOfControl(stateTakeOfControl state_take_of_control, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in state_take_of_control.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = state_take_of_control.execDate.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = state_take_of_control.metadataSystem.href,
                o_from = state_take_of_control.metadataSystem.from.ToString(),
                o_date_takeof = gmt_corr.ToString("s"),
                o_exec_status = "Исполнен : " + gmt_corr.ToString("s"),
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateTakeOfControl(" + state_take_of_control.metadataSystem.from.ToString() + ", " + gmt_corr.ToString("s") + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public void stateStartProcessResponseProcessing(stateStartProcessResponse state_start_process_response, ref Sbapi sbapi, ref string activity)
        {
            bool error = true;//признак ошибок
            foreach (var t in state_start_process_response.metadataSystem.performers)
                if (t == Const_esedo.baiterek_id) { error = false; }
            if (error) { throw new Exception("Baiterek not pointed as receiver!"); }
            DateTime gmt_corr = state_start_process_response.date.AddHours(6);
            BaiterekMessageState baiterekMessageDeliveryState = new BaiterekMessageState()
            {
                o_uuid = state_start_process_response.metadataSystem.href,
                o_from = state_start_process_response.metadataSystem.from.ToString(),
                o_date_start_response = gmt_corr.ToString("s"),
                o_start_response_desc = state_start_process_response.description,
                o_date_refresh = DateTime.Now.ToString("s"),
                o_transaction_history = "stateTakeOfControl(" + state_start_process_response.metadataSystem.from.ToString() + ", " + gmt_corr.ToString("s") + "," + state_start_process_response.description + ") " + DateTime.Now.ToString("s")
            };
            string response;
            try
            {
                response = makeRequestXMLState(baiterekMessageDeliveryState);
                XmlSerializer formatter = new XmlSerializer(typeof(Sbapi), new Type[] { typeof(Header), typeof(Body), typeof(Object_), typeof(Interface), typeof(Message), typeof(Error) });
                using (StringReader fs = new StringReader(response))
                {
                    sbapi = (Sbapi)formatter.Deserialize(fs);
                }
                //sbapi2 = response.XmlDeserializeFromString<Sbapi>();
                if (sbapi.header.error.id != "0")
                {
                    throw new Exception("error id = " + sbapi.header.error.id + " text = " + sbapi.header.error.text);
                }
                if (sbapi.body.object_.text.Substring(0, sbapi.body.object_.text.Length >= 5 ? 5 : sbapi.body.object_.text.Length).ToLower() == "error")
                {
                    throw new Exception(sbapi.body.object_.text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error on call to REST(POST) of Simbase: " + ex.Message);
            }
            activity = sbapi.body.object_.text;
        }
        public string makeRequestNewXML(BaiterekMessageInNew baiterekMessage)
        {
            XElement authElement = new XElement("authdata",
                new XAttribute("msg_id", "1"),
                new XAttribute("user", Const_esedo.userAPI),
                new XAttribute("password", Const_esedo.passwordAPI),
                new XAttribute("msg_type", "5000"),
                new XAttribute("user_ip", "127.0.0.1")
                );
            string auth = authElement.ToString();

            string authBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth));
            XDocument xdoc = new XDocument(
                new XElement("sbapi",
                    new XElement("header",
                        new XElement("interface",
                            new XAttribute("id", Const_esedo.interfaceID_API),
                            new XAttribute("version", "8")
                            ),
                        new XElement("message",
                            new XAttribute("ignore_id", "yes"),
                            new XAttribute("id", "1"),
                            new XAttribute("type", "5000"),
                            new XAttribute("created", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")) //"2019-12-01T14:37:40Z"
                            ),
                        new XElement("error",
                            new XAttribute("id", "0")
                            ),
                        new XElement("auth",
                            new XAttribute("pwd", "hash"), authBase64
                            )
                        ),
                    new XElement("body",
                        new XElement("function",
                            new XAttribute("name", "f_receive_from_esedo1"),
                            from c in baiterekMessage.GetType().GetProperties()
                            where (c.GetValue(baiterekMessage) != null) && (c.GetValue(baiterekMessage).ToString() != "") && c.Name != "files"
                            select
                            new XElement("arg",
                                new XAttribute(
                                    "name",
                                    c.Name),
                                c.GetValue(baiterekMessage)),
                            new XElement("arg",
                                new XAttribute("name", "qty_files"),
                                baiterekMessage.files.Length),
                            from c in baiterekMessage.files
                            select
                            new XElement("arg",
                                new XAttribute(
                                    "name",
                                    "file" + (Array.IndexOf(baiterekMessage.files, c) + 1)), c)
                            )

                        )
                    )
                );

            string apiXMLString = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + xdoc.ToString();
            byte[] requestInFormOfBytes = System.Text.Encoding.UTF8.GetBytes(apiXMLString);
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Const_esedo.orgURL);
            request.Method = "POST";
            request.Timeout = 50000;
            HttpWebResponse response = null;

            request.ContentType = "text/xml;charset=utf-8";
            lock (this.ThisLock)
            {
                using (Stream streamWriter = request.GetRequestStream())
                {
                    streamWriter.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
                }
            }

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            strResponseValue = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"Error\":\"" + ex.Message.ToString() + "\", \"errors\"}";
                throw new Exception(strResponseValue);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;
        }
        public string makeRequestFileListXML(BaiterekMessageResponse baiterekMessageResponse)
        {
            XElement authElement = new XElement("authdata",
                new XAttribute("msg_id", "1"),
                new XAttribute("user", Const_esedo.userAPI),
                new XAttribute("password", Const_esedo.passwordAPI),
                new XAttribute("msg_type", "5000"),
                new XAttribute("user_ip", "127.0.0.1")
                );
            string auth = authElement.ToString();


            string authBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth));
            XDocument xdoc = new XDocument(
                new XElement("sbapi",
                    new XElement("header",
                        new XElement("interface",
                            new XAttribute("id", Const_esedo.interfaceID_API),
                            new XAttribute("version", "8")
                            ),
                        new XElement("message",
                            new XAttribute("ignore_id", "yes"),
                            new XAttribute("id", "1"),
                            new XAttribute("type", "5000"),
                            new XAttribute("created", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")) //"2019-12-01T14:37:40Z"
                            ),
                        new XElement("error",
                            new XAttribute("id", "0")
                            ),
                        new XElement("auth",
                            new XAttribute("pwd", "hash"), authBase64
                            )
                        ),
                    new XElement("body",
                        new XElement("function",
                            new XAttribute("name", "f_write_fileID"),
                            from c in baiterekMessageResponse.GetType().GetProperties()
                            where (c.GetValue(baiterekMessageResponse) != null) && (c.GetValue(baiterekMessageResponse).ToString() != "") && c.Name != "file_id"
                            select
                            new XElement("arg",
                                new XAttribute(
                                    "name",
                                    c.Name),
                                c.GetValue(baiterekMessageResponse)),
                            new XElement("arg",
                                new XAttribute("name", "qty_files"),
                                baiterekMessageResponse?.file_id?.Length ?? 0),
                            from c in baiterekMessageResponse.file_id
                            select
                            new XElement("arg",
                                new XAttribute(
                                    "name",
                                    "file" + (Array.IndexOf(baiterekMessageResponse.file_id, c) + 1)), c.fileId)
                            )

                        )
                    )
                );
            string a = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + xdoc.ToString();
            byte[] requestInFormOfBytes = System.Text.Encoding.UTF8.GetBytes(a);
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Const_esedo.orgURL);
            request.Method = "POST";
            request.Timeout = 500000;
            HttpWebResponse response = null;
            if (request.Method == "POST")
            {
                request.ContentType = "text/xml;charset=utf-8";
                using (Stream streamWriter = request.GetRequestStream())
                {
                    streamWriter.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
                }
            }
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            strResponseValue = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"Error\":\"" + ex.Message.ToString() + "\", \"errors\"}";
                throw new Exception(strResponseValue);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;
        }
        public string makeRequestFilesXML(BaiterekMessageFiles baiterekMessage)
        {
            XElement authElement = new XElement("authdata",
                new XAttribute("msg_id", "1"),
                new XAttribute("user", Const_esedo.userAPI),
                new XAttribute("password", Const_esedo.passwordAPI),
                new XAttribute("msg_type", "3030"),
                new XAttribute("user_ip", "127.0.0.1")
                );
            string auth = authElement.ToString();

            string authBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth));
            XDocument xdoc = new XDocument(
                new XElement("sbapi",
                    new XElement("header",
                        new XElement("interface",
                            new XAttribute("id", Const_esedo.interfaceID_API),
                            new XAttribute("version", "8")
                            ),
                        new XElement("message",
                            new XAttribute("ignore_id", "yes"),
                            new XAttribute("id", "1"),
                            new XAttribute("type", "3030"),
                            new XAttribute("created", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")) //"2019-12-01T14:37:40Z"
                            ),
                        new XElement("error",
                            new XAttribute("id", "0")
                            ),
                        new XElement("auth",
                            new XAttribute("pwd", "hash"), authBase64
                            )
                        ),
                    new XElement("body",
                        new XElement("object",
                            new XAttribute("id", baiterekMessage.id),
                            from c in baiterekMessage.files
                            select
                            new XElement("file",
                                new XAttribute("name", c.name)),
                            from c in baiterekMessage.files
                            select
                            new XElement("file",
                                new XAttribute(
                                    "name",
                                    c.name),
                                c.data)
                            )

                        )
                    )
                );
            string apiXMLString = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + xdoc.ToString();
            byte[] requestInFormOfBytes = System.Text.Encoding.UTF8.GetBytes(apiXMLString);
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Const_esedo.orgURL);
            request.Method = "POST";
            request.Timeout = 50000;
            HttpWebResponse response = null;

            request.ContentType = "text/xml;charset=utf-8";
            lock (this.ThisLock)
            {
                using (Stream streamWriter = request.GetRequestStream())
                {
                    streamWriter.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
                }
            }
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            strResponseValue = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"Error\":\"" + ex.Message.ToString() + "\", \"errors\"}";
                throw new Exception(strResponseValue);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;
        }
        public string makeRequestDocXML(BaiterekMessageIn baiterekMessage)
        {
            XElement authElement = new XElement("authdata",
                new XAttribute("msg_id", "1"),
                new XAttribute("user", Const_esedo.userAPI),
                new XAttribute("password", Const_esedo.passwordAPI),
                new XAttribute("msg_type", "5000"),
                new XAttribute("user_ip", "127.0.0.1")
                );
            string auth = authElement.ToString();

            string authBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth));
            XDocument xdoc = new XDocument(
                new XElement("sbapi",
                    new XElement("header",
                        new XElement("interface",
                            new XAttribute("id", Const_esedo.interfaceID_API),
                            new XAttribute("version", "8")
                            ),
                        new XElement("message",
                            new XAttribute("ignore_id", "yes"),
                            new XAttribute("id", "1"),
                            new XAttribute("type", "5000"),
                            new XAttribute("created", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")) //"2019-12-01T14:37:40Z"
                            ),
                        new XElement("error",
                            new XAttribute("id", "0")
                            ),
                        new XElement("auth",
                            new XAttribute("pwd", "hash"), authBase64
                            )
                        ),
                    new XElement("body",
                        new XElement("function",
                            new XAttribute("name", "f_receive_from_esedo"),
                            from c in baiterekMessage.GetType().GetProperties()
                            where (c.GetValue(baiterekMessage) != null) && (c.GetValue(baiterekMessage).ToString() != "")
                            select
                            new XElement("arg",
                                new XAttribute(
                                    "name",
                                    c.Name),
                                c.GetValue(baiterekMessage))
                            )

                        )
                    )
                );
            string apiXMLString = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + xdoc.ToString();
            byte[] requestInFormOfBytes = System.Text.Encoding.UTF8.GetBytes(apiXMLString);
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Const_esedo.orgURL);
            request.Method = "POST";
            request.Timeout = 50000;
            HttpWebResponse response = null;
            request.ContentType = "text/xml;charset=utf-8";
            lock (this.ThisLock)
            {

                using (Stream streamWriter = request.GetRequestStream())
                {
                    streamWriter.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
                }
            }
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            strResponseValue = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"Error\":\"" + ex.Message.ToString() + "\", \"errors\"}";
                throw new Exception(strResponseValue);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;
        }
        public string makeRequestXMLState(BaiterekMessageState baiterekMessageDeliveryState)
        {
            XElement authElement = new XElement("authdata",
                new XAttribute("msg_id", "1"),
                new XAttribute("user", Const_esedo.userAPI),
                new XAttribute("password", Const_esedo.passwordAPI),
                new XAttribute("msg_type", "5000"),
                new XAttribute("user_ip", "127.0.0.1")
                );
            string auth = authElement.ToString();

            string authBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth));
            XDocument xdoc = new XDocument(
                new XElement("sbapi",
                    new XElement("header",
                        new XElement("interface",
                            new XAttribute("id", Const_esedo.interfaceID_API),
                            new XAttribute("version", "8")),
                        new XElement("message",
                            new XAttribute("ignore_id", "yes"),
                            new XAttribute("id", "1"),
                            new XAttribute("type", "5000"),
                            new XAttribute("created", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"))), //"2019-12-01T14:37:40Z"
                        new XElement("error", new XAttribute("id", "0")),
                        new XElement("auth", new XAttribute("pwd", "hash"), authBase64)),
                    new XElement("body",
                        new XElement("function",
                            new XAttribute("name", "f_delivery_status_esedo"),
                            from c in baiterekMessageDeliveryState.GetType().GetProperties()
                            where (c.GetValue(baiterekMessageDeliveryState) != null) && (c.GetValue(baiterekMessageDeliveryState).ToString() != "")
                            select
                            new XElement("arg",
                                new XAttribute(
                                    "name",
                                    c.Name),
                                c.GetValue(baiterekMessageDeliveryState))
                            )
                        )
                    )
                );
            string a = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + xdoc.ToString();
            byte[] requestInFormOfBytes = System.Text.Encoding.UTF8.GetBytes(a);
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Const_esedo.orgURL);
            request.Method = "POST";
            HttpWebResponse response = null;
            request.ContentType = "text/xml;charset=utf-8";
            lock (this.ThisLock)
            {
                using (Stream streamWriter = request.GetRequestStream())
                {
                    streamWriter.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
                }
            }
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            strResponseValue = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"Error\":[\"" + ex.Message.ToString() + "\"], \"errors\":{}}";
                throw new Exception(strResponseValue);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;
        }
        public string makeRequestXMLPEP(BaiterekMessagePEP baiterekMessagePEP)
        {
            XElement authElement = new XElement("authdata",
                new XAttribute("msg_id", "1"),
                new XAttribute("user", Const_esedo.userAPI),
                new XAttribute("password", Const_esedo.passwordAPI),
                new XAttribute("msg_type", "5000"),
                new XAttribute("user_ip", "127.0.0.1")
                );
            string auth = authElement.ToString();

            string authBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth));
            XDocument xdoc = new XDocument(
                new XElement("sbapi",
                    new XElement("header",
                        new XElement("interface",
                            new XAttribute("id", Const_esedo.interfaceID_API),
                            new XAttribute("version", "8")
                            ),
                        new XElement("message",
                            new XAttribute("ignore_id", "yes"),
                            new XAttribute("id", "1"),
                            new XAttribute("type", "5000"),
                            new XAttribute("created", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")) //"2019-12-01T14:37:40Z"
                            ),
                        new XElement("error",
                            new XAttribute("id", "0")
                            ),
                        new XElement("auth",
                            new XAttribute("pwd", "hash"), authBase64
                            )
                        ),
                    new XElement("body",
                        new XElement("function",
                            new XAttribute("name", "f_receiveAppeal_from_esedo"),
                            from c in baiterekMessagePEP.GetType().GetProperties()
                            where (c.GetValue(baiterekMessagePEP) != null) && (c.GetValue(baiterekMessagePEP).ToString() != "") && c.Name != "files"
                            select
                            new XElement("arg",
                                new XAttribute(
                                    "name",
                                    c.Name),
                                c.GetValue(baiterekMessagePEP)),
                            new XElement("arg",
                                new XAttribute("name", "qty_files"),
                                baiterekMessagePEP.files.Length
                                ),
                            from c in baiterekMessagePEP.files
                            select
                            new XElement("arg",
                                new XAttribute(
                                    "name",
                                    "file" + (Array.IndexOf(baiterekMessagePEP.files, c) + 1)), c)
                            )

                        )
                    )
                );
            string apiXMLString = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + xdoc.ToString();
            byte[] requestInFormOfBytes = System.Text.Encoding.UTF8.GetBytes(apiXMLString);
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Const_esedo.orgURL);
            request.Method = "POST";
            request.Timeout = 50000;
            HttpWebResponse response = null;

            request.ContentType = "text/xml;charset=utf-8";
            lock (this.ThisLock)
            {
                using (Stream streamWriter = request.GetRequestStream())
                {
                    streamWriter.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
                }
            }
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            strResponseValue = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"Error\":[\"" + ex.Message.ToString() + "\"], \"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;
        }
        public static void UpdateDB(ControlTypeWrapper controlTypeWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_control_type> nsi_Control_Types = db.nsi_control_type.ToList();
                List<nsi_control_type> nsi_Control_Types1 = new List<nsi_control_type>();
                foreach (ControlType k in controlTypeWrapper.NSI_CONTROL_TYPE)
                {
                    nsi_control_type nsi_Control_Type = new nsi_control_type()
                    {
                        code = k.code,
                        days = k.days,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Control_Types1.Add(nsi_Control_Type);
                }
                IEnumerable<nsi_control_type> nsi_control_type_inter = nsi_Control_Types.Intersect(nsi_Control_Types1, new ComparerDB<nsi_control_type>());
                db.nsi_control_type.RemoveRange(nsi_control_type_inter);
                db.nsi_control_type.AddRange(nsi_Control_Types1);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(DocumentCharacterWrapper documentCharacterWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_characters> nsi_Characters = db.nsi_characters.ToList();
                List<nsi_characters> nsi_Characters1 = new List<nsi_characters>();
                foreach (DocumentCharacter k in documentCharacterWrapper.NSI_CHARACTERS)
                {
                    nsi_characters nsi_Character = new nsi_characters()
                    {
                        code = k.code,
                        category_code = null,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Characters1.Add(nsi_Character);
                }
                IEnumerable<nsi_characters> nsi_characters_inter = nsi_Characters.Intersect(nsi_Characters1, new ComparerDB<nsi_characters>());
                db.nsi_characters.RemoveRange(nsi_characters_inter);
                db.nsi_characters.AddRange(nsi_Characters1);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(PRActionWrapper pRActionWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_actions> nsi_Actions = db.nsi_actions.ToList();
                List<nsi_actions> nsi_Actions1 = new List<nsi_actions>();
                foreach (PRAction k in pRActionWrapper.NSI_ACTIONS)
                {
                    nsi_actions nsi_Action = new nsi_actions()
                    {
                        code = k.code,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Actions1.Add(nsi_Action);
                }
                IEnumerable<nsi_actions> nsi_actions_inter = nsi_Actions.Intersect(nsi_Actions1, new ComparerDB<nsi_actions>());
                db.nsi_actions.RemoveRange(nsi_actions_inter);
                db.nsi_actions.AddRange(nsi_Actions1);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(PeopleCategoryWrapper peopleCategoryWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_applicant> nsi_Applicants = db.nsi_applicant.ToList();
                List<nsi_applicant> nsi_Applicants1 = new List<nsi_applicant>();
                foreach (PeopleCategory k in peopleCategoryWrapper.NSI_APPLICANT)
                {
                    nsi_applicant nsi_Applicant = new nsi_applicant()
                    {
                        code = k.code,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Applicants1.Add(nsi_Applicant);
                }
                IEnumerable<nsi_applicant> nsi_applicant_inter = nsi_Applicants.Intersect(nsi_Applicants1, new ComparerDB<nsi_applicant>());
                db.nsi_applicant.RemoveRange(nsi_applicant_inter);
                db.nsi_applicant.AddRange(nsi_Applicants1);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(DocumentTypeWrapper documentTypeWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_doc_type> nsi_Doc_Types = db.nsi_doc_type.ToList();
                List<nsi_doc_type> nsi_Doc_Types1 = new List<nsi_doc_type>();
                foreach (DocumentType k in documentTypeWrapper.NSI_DOC_TYPE)
                {
                    nsi_doc_type nsi_Doc_Type = new nsi_doc_type()
                    {
                        code = k.code,
                        category_code = null,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Doc_Types1.Add(nsi_Doc_Type);
                }
                IEnumerable<nsi_doc_type> nsi_doc_type_inter = nsi_Doc_Types.Intersect(nsi_Doc_Types1, new ComparerDB<nsi_doc_type>());
                db.nsi_doc_type.RemoveRange(nsi_doc_type_inter);
                db.nsi_doc_type.AddRange(nsi_Doc_Types1);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(ExecutionResultWrapper executionResultWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_exres_type> nsi_Exres_Types = db.nsi_exres_type.ToList();
                List<nsi_exres_type> nsi_Exres_Types1 = new List<nsi_exres_type>();
                foreach (ExecutionResult k in executionResultWrapper.NSI_EXRES_TYPE)
                {
                    nsi_exres_type nsi_Exres_Type = new nsi_exres_type()
                    {
                        code = k.code,
                        category = k.category,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Exres_Types1.Add(nsi_Exres_Type);
                }
                IEnumerable<nsi_exres_type> nsi_exres_type_inter = nsi_Exres_Types.Intersect(nsi_Exres_Types1, new ComparerDB<nsi_exres_type>());
                db.nsi_exres_type.RemoveRange(nsi_exres_type_inter);
                db.nsi_exres_type.AddRange(nsi_Exres_Types1);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(OrganizationWrapper organizationWrapper)
        {

            using (Model1 db = new Model1())
            {
                List<nsi_org> orgs = db.nsi_org.ToList();
                List<nsi_org> nsi_Orgs = new List<nsi_org>();
                foreach (var k in organizationWrapper.NSI_ORG)
                {
                    nsi_org nsi_Org = new nsi_org()
                    {
                        code = k.code,
                        abbr_kz = k.abbr_kz,
                        abbr_ru = k.abbr_ru,
                        childs_count = Convert.ToInt32(k.ChildsCount),
                        is_esedo = k.isEsedo,
                        is_ready = k.isReady,
                        member = (int)k.member,
                        parent = k.Parent,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru,
                        qg = k.QGIdentifier
                    };
                    nsi_Orgs.Add(nsi_Org);
                }
                IEnumerable<nsi_org> nsi_inter = orgs.Intersect(nsi_Orgs, new ComparerDB<nsi_org>());
                var temp = db.nsi_org.RemoveRange(nsi_inter);
                db.SaveChanges();
                var temp2 = db.nsi_org.AddRange(nsi_Orgs);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(OrganizationTypeWrapper organizationTypeWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_org_types> orgTypes = db.nsi_org_types.ToList();
                List<nsi_org_types> nsi_OrgTypes = new List<nsi_org_types>();

                foreach (var k in organizationTypeWrapper.NSI_ORG_TYPES)
                {
                    nsi_org_types nsi_Org_Types = new nsi_org_types()
                    {
                        code = k.code,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_OrgTypes.Add(nsi_Org_Types);
                }
                IEnumerable<nsi_org_types> nsi_org_types_inter = orgTypes.Intersect(nsi_OrgTypes, new ComparerDB<nsi_org_types>());
                db.nsi_org_types.RemoveRange(nsi_org_types_inter);
                db.nsi_org_types.AddRange(nsi_OrgTypes);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(PostWrapper postWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_pos_type> nsi_Pos_Types = db.nsi_pos_type.ToList();
                List<nsi_pos_type> nsi_Pos_Types1 = new List<nsi_pos_type>();
                foreach (var k in postWrapper.NSI_POS_TYPE)
                {
                    nsi_pos_type nsi_Pos_Type = new nsi_pos_type()
                    {
                        code = k.code,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Pos_Types1.Add(nsi_Pos_Type);
                }
                IEnumerable<nsi_pos_type> nsi_pos_types_inter = nsi_Pos_Types.Intersect(nsi_Pos_Types1, new ComparerDB<nsi_pos_type>());
                db.nsi_pos_type.RemoveRange(nsi_pos_types_inter);
                db.nsi_pos_type.AddRange(nsi_Pos_Types1);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(DocumentReasonWrapper documentReasonWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_reasons> nsi_Reasons = db.nsi_reasons.ToList();
                List<nsi_reasons> nsi_Reasons1 = new List<nsi_reasons>();
                foreach (var k in documentReasonWrapper.NSI_REASONS)
                {
                    nsi_reasons nsi_Reason = new nsi_reasons()
                    {
                        code = k.code,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Reasons1.Add(nsi_Reason);
                }
                IEnumerable<nsi_reasons> nsi_reasons_inter = nsi_Reasons.Intersect(nsi_Reasons1, new ComparerDB<nsi_reasons>());
                db.nsi_reasons.RemoveRange(nsi_reasons_inter);
                db.nsi_reasons.AddRange(nsi_Reasons1);
                db.SaveChanges();
            }
        }
        public static void UpdateDB(RegionWrapper regionWrapper)
        {
            using (Model1 db = new Model1())
            {
                List<nsi_regions> nsi_Regions = db.nsi_regions.ToList();
                List<nsi_regions> nsi_Regions1 = new List<nsi_regions>();
                foreach (var k in regionWrapper.NSI_REGIONS)
                {
                    nsi_regions nsi_Region = new nsi_regions()
                    {
                        code = k.code,
                        region_type = k.region_type,
                        guid = Convert.ToInt32(k.ElementGUID),
                        is_marked_to_delete = k.IsMarkedToDelete,
                        update_date = k.update_date,
                        id = Convert.ToInt32(k.Id),
                        name_kz = k.name_kz,
                        name_ru = k.name_ru
                    };
                    nsi_Regions1.Add(nsi_Region);
                }
                IEnumerable<nsi_regions> nsi_regions_inter = nsi_Regions.Intersect(nsi_Regions1, new ComparerDB<nsi_regions>());
                db.nsi_regions.RemoveRange(nsi_regions_inter);
                db.nsi_regions.AddRange(nsi_Regions1);
                db.SaveChanges();
            }
        }

        public string GetDictionary(NsiSync.GetDictionaryRequest getDictionaryRequest)
        {
            NsiSync.DictionaryPortClient dictionaryPortClient = new NsiSync.DictionaryPortClient();
            NsiSync.GetDictionaryResponse getDictionaryResponse = new GetDictionaryResponse();

            if (getDictionaryRequest.sender == Const_esedo.sender)
            {
                //try
                {
                    ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
                    getDictionaryResponse = dictionaryPortClient.GetDictionary(getDictionaryRequest);
                    foreach (object i in getDictionaryResponse.data)
                    {
                        Type j = i.GetType();
                        switch (j.Name)
                        {
                            case "ControlTypeWrapper":
                                UpdateDB((ControlTypeWrapper)i);
                                break;
                            case "DocumentCharacterWrapper":
                                UpdateDB((DocumentCharacterWrapper)i);
                                break;
                            case "PRActionWrapper":
                                UpdateDB((PRActionWrapper)i);
                                break;
                            case "PeopleCategoryWrapper":
                                UpdateDB((PeopleCategoryWrapper)i);
                                break;
                            case "DocumentTypeWrapper":
                                UpdateDB((DocumentTypeWrapper)i);
                                break;
                            case "ExecutionResultWrapper":
                                UpdateDB((ExecutionResultWrapper)i);
                                break;
                            case "OrganizationWrapper":
                                UpdateDB((OrganizationWrapper)i);
                                break;
                            case "OrganizationTypeWrapper":
                                UpdateDB((OrganizationTypeWrapper)i);
                                break;
                            case "PostWrapper":
                                UpdateDB((PostWrapper)i);
                                break;
                            case "DocumentReasonWrapper":
                                UpdateDB((DocumentReasonWrapper)i);
                                break;
                            case "RegionWrapper":
                                UpdateDB((RegionWrapper)i);
                                break;
                        }
                    }
                }
                //catch (Exception ex)
                //{
                //    return ex.Message;
                //}
            }
            return getDictionaryResponse.data.Length.ToString();
        }

        public string wordDocVersion(WordBase64File wordFileBase64)
        {
            System.Xml.Linq.XDocument coreFileProperties;
            System.Xml.Linq.XDocument extFileProperties;
            System.Xml.Linq.XDocument customFileProperties;

            byte[] file = String.IsNullOrEmpty(wordFileBase64.base64_data)? null: Convert.FromBase64String(wordFileBase64.base64_data);
            if (file != null)
            {
                Stream stream = new MemoryStream(file);
                using (DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordDoc = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(stream, false))
                {
                    // Load the properties from the XML files into the XDocument objects
                    DocumentFormat.OpenXml.Packaging.CoreFilePropertiesPart corePart = wordDoc.CoreFilePropertiesPart;
                    coreFileProperties = System.Xml.Linq.XDocument.Load(corePart.GetStream());
                    DocumentFormat.OpenXml.Packaging.ExtendedFilePropertiesPart appPart = wordDoc.ExtendedFilePropertiesPart;
                    DocumentFormat.OpenXml.Packaging.CustomFilePropertiesPart customPart = wordDoc.CustomFilePropertiesPart;
                    extFileProperties = System.Xml.Linq.XDocument.Load(appPart.GetStream());
                    customFileProperties = System.Xml.Linq.XDocument.Load(customPart.GetStream());
                    foreach (System.Xml.Linq.XElement ele in customFileProperties.Root.Descendants())
                    {
                        if ((ele.Attribute("name")?.Value ?? ele.Name.LocalName )== "Номер документа")
                        {
                            return ele.Value;
                            // ele.Name.LocalName, ele.Value;
                        }
                    }
                }
                
            }
            return "no version";
        }

        public BaiterekMessageOutNew GatewayTest(BaiterekMessageOutNew baiterekMessage)
        {
            return baiterekMessage;
        }
    }
}

