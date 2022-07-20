using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc
{
    public static class Const_esedo
    {
        public static string sender { get; } = "baiterek";
        public static string receiver_esedo { get; } = "esedo";
        public static long high { get; set; } = 10_000_000; //11_000_000;
        public static long baiterek_id { get; } = 17474966;
        public static long qujatgateway_id { get; } = 17466323;
        public static long baiterek_guid { get; } = 2302782;
        public static long qujatgateway_guid { get; } = 2294076;
        public static string orgURL { get; } = "https://bpm-api.baiterek.gov.kz/";
        public static string userAPI { get; } = "api.sign3";
        public static string passwordAPI { get; } = "cc9f816a42431cf852cdc7a3fad42a6f65ffce24";
        public static string interfaceID_API { get; } = "201596931";

        public static int localityPEP { get; } = 584;
        public static int individualPEP { get; } = 573;

        public static QGClient[] qGClients { get; } = new QGClient[]
        {
            new QGClient() { ID = 17476086, receiver_code = "acc" },
            new QGClient() { ID = 17476087, receiver_code = "acc" },
            new QGClient() { ID = 17476088, receiver_code = "acc" },
            new QGClient() { ID = 17476089, receiver_code = "acc" },
            new QGClient() { ID = 17476091, receiver_code = "acc" },
            new QGClient() { ID = 17476092, receiver_code = "acc" },
            new QGClient() { ID = 17476093, receiver_code = "acc" },
            new QGClient() { ID = 17476094, receiver_code = "acc" },
            new QGClient() { ID = 17476094, receiver_code = "acc" },
            new QGClient() { ID = 17476095, receiver_code = "acc" },
            new QGClient() { ID = 17476096, receiver_code = "acc" },
            new QGClient() { ID = 17476097, receiver_code = "acc" },
            new QGClient() { ID = 17476098, receiver_code = "acc" },
            new QGClient() { ID = 17476099, receiver_code = "acc" },
            new QGClient() { ID = 17476100, receiver_code = "acc" },
            new QGClient() { ID = 17476101, receiver_code = "acc" },
            new QGClient() { ID = 17476102, receiver_code = "acc" },
            new QGClient() { ID = 17476103, receiver_code = "acc" },
            new QGClient() { ID = 17474966, receiver_code = "baiterek" },
            new QGClient() { ID = 17474503, receiver_code = "gfss" },
            new QGClient() { ID = 17475182, receiver_code = "gfss" },
            new QGClient() { ID = 17475181, receiver_code = "gfss" },
            new QGClient() { ID = 17475180, receiver_code = "gfss" },
            new QGClient() { ID = 17475179, receiver_code = "gfss" },
            new QGClient() { ID = 17475178, receiver_code = "gfss" },
            new QGClient() { ID = 17475177, receiver_code = "gfss" },
            new QGClient() { ID = 17475176, receiver_code = "gfss" },
            new QGClient() { ID = 17475175, receiver_code = "gfss" },
            new QGClient() { ID = 17475174, receiver_code = "gfss" },
            new QGClient() { ID = 17475173, receiver_code = "gfss" },
            new QGClient() { ID = 17475172, receiver_code = "gfss" },
            new QGClient() { ID = 17475171, receiver_code = "gfss" },
            new QGClient() { ID = 17475170, receiver_code = "gfss" },
            new QGClient() { ID = 17475169, receiver_code = "gfss" },
            new QGClient() { ID = 17475168, receiver_code = "gfss" },
            new QGClient() { ID = 17475167, receiver_code = "gfss" },
            new QGClient() { ID = 17475166, receiver_code = "gfss" },
            new QGClient() { ID = 17474160, receiver_code = "rcsc" },
            new QGClient() { ID = 17475613, receiver_code = "iturkistan_sch23" },
            new QGClient() { ID = 17473516, receiver_code = "khc" }
        };
        public class QGClient
        {
            public long ID { get; set; }
            public string receiver_code { get; set; }
        }
    }
}
