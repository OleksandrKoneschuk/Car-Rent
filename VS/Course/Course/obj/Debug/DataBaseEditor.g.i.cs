﻿#pragma checksum "..\..\DataBaseEditor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "29621A9F08670FA748B2E34F88DF6C012C71D07C695FAD386838680D35FD3CB3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Course;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Course {
    
    
    /// <summary>
    /// DataBaseEditor
    /// </summary>
    public partial class DataBaseEditor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox showComboBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTextBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_GoToAdminMenu;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridTime;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox timeTextBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GetDateTime;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridId;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox idTextBox;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GetId;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_forUpdate;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveChangesButton;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\DataBaseEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Delete;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Course;component/databaseeditor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DataBaseEditor.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 18 "..\..\DataBaseEditor.xaml"
            this.dataGrid.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.DataGrid_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.showComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 26 "..\..\DataBaseEditor.xaml"
            this.showComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.showComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.searchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\DataBaseEditor.xaml"
            this.searchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.searchTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.button_GoToAdminMenu = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\DataBaseEditor.xaml"
            this.button_GoToAdminMenu.Click += new System.Windows.RoutedEventHandler(this.button_GoToAdminMenu_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.gridTime = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.timeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.GetDateTime = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\DataBaseEditor.xaml"
            this.GetDateTime.Click += new System.Windows.RoutedEventHandler(this.GetDateTime_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.gridId = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.idTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.GetId = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\DataBaseEditor.xaml"
            this.GetId.Click += new System.Windows.RoutedEventHandler(this.GetIdKlient_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.textBox_forUpdate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.SaveChangesButton = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\DataBaseEditor.xaml"
            this.SaveChangesButton.Click += new System.Windows.RoutedEventHandler(this.SaveChangesButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.button_Delete = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\DataBaseEditor.xaml"
            this.button_Delete.Click += new System.Windows.RoutedEventHandler(this.button_Delete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

