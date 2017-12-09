using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Entities
{
    public class SubFingerprintEntity
    {
        public long Id { get; set; } // Id (Primary key)
        public int TrackId { get; set; } // TrackId
        public int SequenceNumber { get; set; } // SequenceNumber
        public double SequenceAt { get; set; } // SequenceAt
        public long HashTable0 { get; set; } // HashTable_0
        public long HashTable1 { get; set; } // HashTable_1
        public long HashTable2 { get; set; } // HashTable_2
        public long HashTable3 { get; set; } // HashTable_3
        public long HashTable4 { get; set; } // HashTable_4
        public long HashTable5 { get; set; } // HashTable_5
        public long HashTable6 { get; set; } // HashTable_6
        public long HashTable7 { get; set; } // HashTable_7
        public long HashTable8 { get; set; } // HashTable_8
        public long HashTable9 { get; set; } // HashTable_9
        public long HashTable10 { get; set; } // HashTable_10
        public long HashTable11 { get; set; } // HashTable_11
        public long HashTable12 { get; set; } // HashTable_12
        public long HashTable13 { get; set; } // HashTable_13
        public long HashTable14 { get; set; } // HashTable_14
        public long HashTable15 { get; set; } // HashTable_15
        public long HashTable16 { get; set; } // HashTable_16
        public long HashTable17 { get; set; } // HashTable_17
        public long HashTable18 { get; set; } // HashTable_18
        public long HashTable19 { get; set; } // HashTable_19
        public long HashTable20 { get; set; } // HashTable_20
        public long HashTable21 { get; set; } // HashTable_21
        public long HashTable22 { get; set; } // HashTable_22
        public long HashTable23 { get; set; } // HashTable_23
        public long HashTable24 { get; set; } // HashTable_24
        public string Clusters { get; set; } // Clusters (length: 255)

        // Foreign keys

        /// <summary>
        /// Parent Track pointed by [SubFingerprints].([TrackId]) (FK_SubFingerprints_Tracks)
        /// </summary>
        public virtual TrackEntity Track { get; set; } // FK_SubFingerprints_Tracks
    }
}
