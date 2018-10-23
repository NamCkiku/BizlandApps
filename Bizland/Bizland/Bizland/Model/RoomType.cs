using Bizland.ViewModels;
using System;
using Xamarin.Forms;

namespace Bizland.Model
{
    public class RoomType : ExtendedBindableObject
    {
        public int ID { get; set; }

        public string RoomTypeName { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public int? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        public string ImageIcon { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public bool isDelete { get; set; }

        public bool Status { get; set; }

        private Color _Selected = Color.White;
        public Color Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }
    }
}
