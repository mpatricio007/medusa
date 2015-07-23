﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    [ComplexType]
    [Serializable]
    public class Email
    {
        [MaxLength(100)]
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        public string value { get; set; }

        public Email()
        {
            
        }
        
        
        public Email(string strValue)
        {
            value = strValue;        
        }

        public static implicit operator Email(PessoaEmail pe)
        {
            return pe.email;
        }
    }
}