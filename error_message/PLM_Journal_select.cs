﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace error_message
{
   public class PLM_Journal_select
    {
        #region 字段信息

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自增长列
        public int id { get; set; }







        /// <summary>
        public string Interface_parameter { get; set; }







        /// <summary>
        public string Interface_name { get; set; }

        /// <summary>
        public int Error_num { get; set; }

        /// <summary>
        public int mail { get; set; }

        /// <summary>
        public DateTime mail_date { get; set; }







        /// <summary>
        public string mailing_address { get; set; }








        /// <summary>
        public string error_message { get; set; }

        #endregion

    }
}