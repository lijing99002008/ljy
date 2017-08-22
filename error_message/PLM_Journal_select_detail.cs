﻿using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace error_message
{
    class PLM_Journal_select_detail
    {
        #region 字段信息
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自增长列
        public int id { get; set; }

        /// <summary>
        public DateTime time { get; set; }







        /// <summary>
        public string error_message { get; set; }







        /// <summary>
        public string Interface_name { get; set; }







        /// <summary>
        public string Interface_parameter { get; set; }
        #endregion

    }
}