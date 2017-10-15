// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.Toolkit.Uwp.UI.Controls
{
    internal class HamburgerMenuNavViewItemTemplateSelector : DataTemplateSelector
    {
        private HamburgerMenu _hamburgerMenu;

        internal HamburgerMenuNavViewItemTemplateSelector(HamburgerMenu hamburgerMenu)
        {
            _hamburgerMenu = hamburgerMenu;
        }

        private DataTemplate SelectItemTemplate(object item)
        {
            if (_hamburgerMenu == null)
            {
                return TemplateFromItemTemplateSelector(item);
            }

            var items = _hamburgerMenu.ItemsSource as IEnumerable<object>;

            if (items != null && items.Contains(item))
            {
                return TemplateFromItemTemplateSelector(item);
            }

            if (_hamburgerMenu.OptionsItemTemplate != null)
            {
                var options = _hamburgerMenu.OptionsItemsSource as IEnumerable<object>;
                if (options != null && options.Contains(item))
                {
                    return TemplateFromOptionsItemTemplateSelector(item);
                }
            }

            return TemplateFromItemTemplateSelector(item);
        }

        private DataTemplate TemplateFromItemTemplateSelector(object item)
        {
            if (_hamburgerMenu.ItemTemplateSelector != null)
            {
                return _hamburgerMenu.ItemTemplateSelector.SelectTemplate(item);
            }

            return _hamburgerMenu.ItemTemplate;
        }

        private DataTemplate TemplateFromOptionsItemTemplateSelector(object item)
        {
            if (_hamburgerMenu.OptionsItemTemplateSelector != null)
            {
                return _hamburgerMenu.OptionsItemTemplateSelector.SelectTemplate(item);
            }

            return _hamburgerMenu.OptionsItemTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return SelectItemTemplate(item);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectItemTemplate(item);
        }
    }
}
