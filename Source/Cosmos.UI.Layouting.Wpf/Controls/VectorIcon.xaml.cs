using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cosmos.UI.Layoutting.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for VectorIcon.xaml
    /// </summary>
    public class VectorIconConst : Image
    {
        public VectorIconConst()
        {

        }

        public VectorIconConst(String drawing_resource_key)
            : this()
        {
            SetResourceReference(SourceProperty, drawing_resource_key);
        }

    }

    public partial class VectorIcon : UserControl
    {
        public VectorIcon()
            : this(null)
        {

        }

        public VectorIcon(String geometry_key)
            : this(geometry_key, "CosmosIcon.Static.Fill", "CosmosIcon.MouseOver.Fill")
        {

        }

        public VectorIcon(String geometry_key, String fill_resouce_key, String hover_fill_resource_key)
        {
            InitializeComponent();
            SetResourceReference(IconGeometryProperty, geometry_key);
            SetResourceReference(IconFillProperty, fill_resouce_key);
            SetResourceReference(IconHoverFillProperty, hover_fill_resource_key);
        }

        public Brush IconFill
        {
            get { return (Brush)GetValue(IconFillProperty); }
            set { SetValue(IconFillProperty, value); }
        }

        public static readonly DependencyProperty IconFillProperty =
               DependencyProperty.Register("IconFill", typeof(Brush), typeof(VectorIcon),
                   new FrameworkPropertyMetadata(Brushes.Gray));

        public Brush IconHoverFill
        {
            get { return (Brush)GetValue(IconHoverFillProperty); }
            set { SetValue(IconHoverFillProperty, value); }
        }

        public static readonly DependencyProperty IconHoverFillProperty =
               DependencyProperty.Register("IconHoverFill", typeof(Brush), typeof(VectorIcon),
                   new FrameworkPropertyMetadata(Brushes.Cyan));


        public bool IsEnableHoverChange
        {
            get { return (bool)GetValue(IsEnableHoverChangeProperty); }
            set { SetValue(IsEnableHoverChangeProperty, value); }
        }

        public static readonly DependencyProperty IsEnableHoverChangeProperty =
               DependencyProperty.Register("IsEnableHoverChange", typeof(bool), typeof(VectorIcon),
                   new FrameworkPropertyMetadata(false));

        public Geometry IconGeometry
        {
            get { return (Geometry)GetValue(IconGeometryProperty); }
            set { SetValue(IconGeometryProperty, value); }
        }

        public static readonly DependencyProperty IconGeometryProperty =
               DependencyProperty.Register("IconGeometry", typeof(Geometry), typeof(VectorIcon),
                   new FrameworkPropertyMetadata(IconGeometryChangedCallback));

        private static void IconGeometryChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VectorIcon vector_icon = (VectorIcon)d;
            vector_icon.icon_path.Data = (Geometry)e.NewValue;
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
               DependencyProperty.Register("Text", typeof(String), typeof(VectorIcon),
                   new FrameworkPropertyMetadata(String.Empty, TextChangedCallback));

        private static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VectorIcon icon = (VectorIcon)d;
            icon.icon_text.Text = (String)e.NewValue;
            if (icon.icon_text.Text == String.Empty)
            {
                icon.icon_text.Visibility = Visibility.Collapsed;
            }
            else
            {
                icon.icon_text.Visibility = Visibility.Visible;
            }
        }
    }
}
