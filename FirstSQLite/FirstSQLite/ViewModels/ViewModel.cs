using FirstSQLite.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FirstSQLite;

namespace FirstSQLite.ViewModels
{
    class ViewModel:ViewModelBase
    {
        private string newBlogText;
        public string NewBlogText
        {
            get { return this.newBlogText; }
            set
            {
                this.newBlogText = value;
                OnPropertyChanged(nameof(NewBlogText));
            }
        }

        private List<Blog> itemSources;
        public List<Blog> ItemSources
        {
            get
            {
                using (var db = new BloggingContext())
                {
                    var souce = db.Blogs.ToList();
                    return souce;
                }
            }
            set
            {
                this.itemSources = value;
                OnPropertyChanged(nameof(ItemSources));
            }
        }


        public ICommand AddCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    using (var db = new BloggingContext())
                    {
                        var blog = new Blog { Url=this.NewBlogText };
                        db.Blogs.Add(blog);
                        db.SaveChanges();

                        //Addを押したらTextBoxが空になる様にした
                        this.newBlogText = "";
                        OnPropertyChanged(nameof(NewBlogText));

                        this.ItemSources = db.Blogs.ToList();
                    }
                });
            }
        }

        private int selectedIndexes;
        public int SelectedIndexes
        {
            get
            { return this.selectedIndexes; }
            set
            {
                this.selectedIndexes = value;
                OnPropertyChanged(nameof(SelectedIndexes));
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    using (var db = new BloggingContext())
                    {
                        var itmssource = ItemSources as List<Blog>;
                        if (SelectedIndexes == -1) return;
                        var blog = itmssource[SelectedIndexes];

                        if (blog != null)
                        {
                            db.Blogs.Remove(blog);
                            db.SaveChanges();

                            this.ItemSources = db.Blogs.ToList();
                        }
                    }
                });
            }
        }
    }
}
