﻿#pragma checksum "..\..\ChooseCarWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B68A8A76E1A4004F1530D930C5C304ACC806F18FD85B31B8E36A2663B8AC9D03"
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
    /// ChooseCarWindow
    /// </summary>
    public partial class ChooseCarWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\ChooseCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Start_Date_DatePicker;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\ChooseCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker end_Of_Rental_DatePicker;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ChooseCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Close;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ChooseCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cost_TextBox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\ChooseCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Submit;
        
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
            System.Uri resourceLocater = new System.Uri("/Course;component/choosecarwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChooseCarWindow.xaml"
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
            this.Start_Date_DatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 2:
            this.end_Of_Rental_DatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.button_Close = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\ChooseCarWindow.xaml"
            this.button_Close.Click += new System.Windows.RoutedEventHandler(this.button_Close_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cost_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.button_Submit = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\ChooseCarWindow.xaml"
            this.button_Submit.Click += new System.Windows.RoutedEventHandler(this.button_Submit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

