using DataAccess.Models.IPDModel;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Bussiness.IPD
{
    public class BradenScaleManager
    {
        protected static IUnitOfWork unitOfWork = new EfUnitOfWork();

        /// <summary>
        /// Get BradenScale by primary key id
        /// </summary>
        /// <param name="vitalsignId"></param>
        /// <returns></returns>
        public static BradenScale GetById(Guid vitalsignId)
        {
            return unitOfWork.IPDBradenScaleRepository.GetById(vitalsignId);
        }


        public static DataSet GetByVisitId(BradenScale bradenScale)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("VISIT_ID", bradenScale.VisitId);
            dict.Add("USER_NAME", bradenScale.CreatedBy);
            dict.Add("FROM_DATE", bradenScale.DateFrom);
            dict.Add("TO_DATE", bradenScale.DateTo);
            dict.Add("PAGE_INDEX", bradenScale.PageIndex);
            dict.Add("PAGE_SIZE", bradenScale.PageSize);
            dict.Add("TOTAL_ROW", 0);

            EfUnitOfWork unitOfWork = new EfUnitOfWork();
            var ds = unitOfWork.ExecuteDataSet("prc_BradenScale_SearchByVisitId", dict);

            return ds;
        }


        public static int Update(BradenScale entity)
        {
            try
            {
                unitOfWork.IPDBradenScaleRepository.Update(entity);
                unitOfWork.Commit();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
    }
}
