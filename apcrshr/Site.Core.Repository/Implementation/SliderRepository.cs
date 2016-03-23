using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class SliderRepository : ISliderRepository
    {
        public Slider FindByActionURL(string URL)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Sliders.Where(i => i.URL.Equals(URL)).SingleOrDefault();
            }
        }

        public Slider FindByTitle(string title)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Sliders.Where(i => i.Title.Equals(title)).SingleOrDefault();
            }
        }

        public IList<Slider> FindTop(int top)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Sliders.OrderByDescending(i => i.CreatedDate).Take(top).ToList();
            }
        }

        public Tuple<int, IList<Slider>> FindAll(int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var count = context.Sliders.Count();
                var items = context.Sliders.OrderByDescending(i => i.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Tuple.Create<int, IList<Slider>>(count, items);
            }
        }

        public Tuple<int, IList<Slider>> FindAllRelated(DateTime date, int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var items = context.Sliders.OrderByDescending(n => n.CreatedDate).Where(n => n.CreatedDate < date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.Sliders.OrderByDescending(n => n.CreatedDate).Where(n => n.CreatedDate < date).Count();
                return Tuple.Create<int, IList<Slider>>(count, items);
            }
        }

        public IList<Slider> Search(string key)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Sliders.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
                new SqlParameter("@stringToFind", key),
                new SqlParameter("@schema", "dbo"),
                new SqlParameter("@table", "Slider")).ToList();
            }
        }

        public object Insert(Slider item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Sliders.Add(item);
                context.SaveChanges();
                return item.SliderID;
            }
        }

        public void Update(Slider item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var slider = context.Sliders.Where(i => i.SliderID.Equals(item.SliderID)).SingleOrDefault();
                if (slider != null)
                {
                    slider.Title = item.Title;
                    slider.URL = item.URL;
                    if (!string.IsNullOrEmpty(item.URL))
                    {
                        slider.URL = item.URL;
                    }
                    if (!string.IsNullOrEmpty(item.ImageURL))
                    {
                        slider.ImageURL = item.ImageURL;
                    }
                    slider.UpdatedBy = item.UpdatedBy;
                    slider.UpdatedDate = DateTime.Now;
                    slider.Language = item.Language;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Slider id {0} invalid", slider.SliderID));

                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var slider = context.Sliders.Where(i => i.SliderID.Equals(_id)).SingleOrDefault();
                if (slider != null)
                {
                    context.Sliders.Remove(slider);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Slider id {0} invalid", id));
                }
            }
        }

        public Slider FindByID(object id)
        {
            var _id = id.ToString();
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Sliders.Where(i => i.SliderID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Slider> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Sliders.ToList();
            }
        }
    }
}
