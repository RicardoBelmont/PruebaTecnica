using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Entities
{
    public interface IResponseBase<T>
    {
        /// <summary>
        /// Status response
        /// </summary>
        HttpStatusCode status { get; set; }

        /// <summary>
        /// Payload
        /// </summary>
        T Response { get; set; }
    }
}
