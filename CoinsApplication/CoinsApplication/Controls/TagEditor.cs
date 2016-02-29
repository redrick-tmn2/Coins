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

namespace CoinsApplication.Controls
{
    public class TagEditor : TextBox 
    {
        static TagEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TagEditor), new FrameworkPropertyMetadata(typeof(TagEditor)));

        }



        public DataTemplate TokenTemplate
        {
            get { return (DataTemplate)GetValue(TokenTemplateProperty); }
            set { SetValue(TokenTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TokenTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TokenTemplateProperty =
            DependencyProperty.Register("TokenTemplate", typeof(DataTemplate), typeof(TagEditor), new PropertyMetadata(null));

        
        
        public Func<string, string> TokenMatcher = text =>
                               {
                                   if (text.EndsWith(";"))
                                   {
                                       // Remove the ';'
                                       return text.Substring(0, text.Length - 1).Trim().ToUpper();
                                   }
  
                                   return null;
                              };

        public TagEditor()
        {
            TextChanged += OnTokenTextChanged;
        }
  
        private void OnTokenTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = this.Text;//CaretPosition.GetTextInRun(LogicalDirection.Backward);
            if (TokenMatcher != null)
            {
                var token = TokenMatcher(text);
                if (token != null)
                {
                    ReplaceTextWithToken(text, token);
                }
            }
        }
 
        private void ReplaceTextWithToken(string inputText, object token)
        {
            // Remove the handler temporarily as we will be modifying tokens below, causing more TextChanged events
            TextChanged -= OnTokenTextChanged;

           // inputText.Split()

            TextChanged += OnTokenTextChanged;
        }

        private InlineUIContainer CreateTokenContainer(string inputText, object token)
        {
            // Note: we are not using the inputText here, but could be used in future

            var presenter = new ContentPresenter
            {
                Content = token,
                ContentTemplate = TokenTemplate,
            };

            // BaselineAlignment is needed to align with Run
            return new InlineUIContainer(presenter) {BaselineAlignment = BaselineAlignment.TextBottom};
        }

    }
}
