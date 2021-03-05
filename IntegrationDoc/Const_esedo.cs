using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc
{
    public static class Const_esedo
    {
        public static long high { get; set; } = 11_000_000; //11_000_000;
        public static long baiterek_id { get; } = 17474966;
        public static long qujatgateway_id { get; } = 17466323;
        public static long baiterek_guid { get; } = 2302782;
        public static long qujatgateway_guid { get; } = 2294076;
        public static string orgURL { get; } = "https://bpm-api.baiterek.gov.kz/";
        public static string userAPI { get; } = "api.sign3";
        public static string passwordAPI { get; } = "cc9f816a42431cf852cdc7a3fad42a6f65ffce24";
        public static string interfaceID_API { get; } = "201596931";
        public static int kazLang { get; } = 426;
        public static int rusLang { get; } = 427;
        public static int kazRusLang { get; } = 428;
        public static int engLang { get; } = 429;
        public static int otherLang { get; } = 430;
        public static int rezidentPEP { get; } = 7951;
        public static int nonRezidentPEP { get; } = 7952;
        public static int localityPEP { get; } = 584;
        public static int individualPEP { get; } = 573;
    }
}