   M S B A M L   `   `   `  ����  
ToDoHWork2$    ToDoHWork2.Modal.RadMessageBox   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   f bTelerik.Windows.Documents, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7m iTelerik.Windows.Controls.Docking, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7w sTelerik.Windows.Controls.DataVisualization, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e	 aTelerik.Windows.Controls, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p
 lTelerik.Windows.Controls.Navigation, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Documents.Core, Version=2019.2.503.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
  5   	   -   5        ( 
 "Telerik.Windows.Controls.RadWindow   Name$  radMessageBoxWnd��5   b   radMessageBoxWnd��$	��140��6	   $	��300��6      WindowStartupLocation$ CenterOwner=�6(   ) 
 #Telerik.Windows.Controls.WindowBase   
ResizeMode$ NoResize=�6L   + 	 %Telerik.Windows.Controls.StyleManager   Theme- 	 'Telerik.Windows.Controls.ThemeConverter$ Fluent 6|    � 	Resources 5         imgControlTemplate ��Name �� Triggers �� Binding �� Value$  System.Windows.MessageBoxImage	 �� 
TargetName
 �� Property �� Value  btnControlTemplate � RowDefinitions � ColumnDefinitions �� Orientation( 	 "Telerik.Windows.Controls.RadButton �� 	IsDefault �� IsCancel%  System.Windows.MessageBoxButton��%�  &	        &	 �	    �� 5   
   ControlTemplate_1��.R�5      �� $ 
imgDlgType��6'   
imgDlgType��$��5,5,5,5q�6;    5      �� 5      DataTrigger_1�� 64   �� *DialogImage+ 5       5      MessageBoxImage_1��Asterisk=�5      .a�5      �� Setter_1��$	 
imgDlgType��6.   
 � ��$2 ,/ToDoHWork2;component/Resources/asterisk.png��6X   5      �� 5      DataTrigger_2�� 64   �� *DialogImage+ 5       5      MessageBoxImage_2��	Error=�5      .a�5      �� Setter_2��$	 
imgDlgType��6.   
 � ��$/ )/ToDoHWork2;component/Resources/error.png��6X   5      �� 5       DataTrigger_3�� 64   �� *DialogImage+ 5!       5"      MessageBoxImage_3��Exclamation=�5$      .a�5&      �� Setter_3��$	 
imgDlgType��6.   
 � ��$5 //ToDoHWork2;component/Resources/exclamation.png��6X   5'      �� 5)      DataTrigger_4�� 64   �� *DialogImage+ 5*       5+      MessageBoxImage_4��Hand=�5-      .a�5/      �� Setter_4��$	 
imgDlgType��6.   
 � ��$. (/ToDoHWork2;component/Resources/hand.png��6X   50      �� 52      DataTrigger_5�� 64   �� *DialogImage+ 53       54      MessageBoxImage_5��Information=�56      .a�58      �� Setter_5��$	 
imgDlgType��6.   
 � ��$5 //ToDoHWork2;component/Resources/information.png��6X   59      �� 5;      DataTrigger_6�� 64   �� *DialogImage+ 5<       5=      MessageBoxImage_6��None=�5?      .a�5A      �� Setter_6��$	 
imgDlgType��6.   
 � ��$. (/ToDoHWork2;component/Resources/none.png��6X   5B      �� 5D      DataTrigger_7�� 64   �� *DialogImage+ 5E       5F      MessageBoxImage_7��Question=�5H      .a�5J      �� Setter_7��$	 
imgDlgType��6.   
 � ��$2 ,/ToDoHWork2;component/Resources/question.png��6X   5K      �� 5M      DataTrigger_8�� 64   �� *DialogImage+ 5N       5O      MessageBoxImage_8��Stop=�5Q      .a�5S      �� Setter_8��$	 
imgDlgType��6.   
 � ��$. (/ToDoHWork2;component/Resources/stop.png��6X   5T      �� 5V      DataTrigger_9�� 64   �� *DialogImage+ 5W       5X      MessageBoxImage_9��Warning=�5Z      .a�5\      �� Setter_9��$	 
imgDlgType��6.   
 � ��$1 +/ToDoHWork2;component/Resources/warning.png��6X   5]      5_      �� 5c   
   ControlTemplate_2��.R�5d      � $��grdBtnControls��6*   grdBtnControls��$��0,0,0,5q�6@    5e      �� 5f      RowDefinition_1��$
��Auto �6<    5h      �� 5i      ColumnDefinition_1��$
��Auto �6B   .+�5k      �� StackPanel_1��$��Bottom=�62   $��Right=�6M   $ 
Horizontal=�6i   $��0��6�   $
��Auto��6�   .+�5m        $��btnYes��67   -   5        
btnYes��$	��Yes��5m   E   $��5,0,5,0q�6S   $��70��6d   $��Center=�6o   $��Center=�6�   $y�	Collapsed=�6�   $ Fluent 6�     5n      $��btnNo��66   -   5        	btnNo��$��No��5n   C   $��5,0,5,0q�6P   $��70��6a   $��Center=�6l   $��Center=�6�   $y�	Collapsed=�6�   $ Fluent 6�     5p      $��btnOk��66   -   5        	btnOk��$��Ok��5p   C   $��5,0,5,0q�6P   $��70��6a   $��Center=�6l   $��Center=�6�    . $ Fluent 6�     5q      $��	btnCancel��6:   -   5        	btnCancel��$��Cancel��5q   K   $��5,0,5,0q�6\   $��70��6m   $��Center=�6x   $��Center=�6�   $y�	Collapsed=�6�    . $ Fluent 6�   5r      5s       5t      �� 5u      DataTrigger_10�� 65   �� *	Buttons+ 5v       5w      MessageBoxButton_1��OK=�5y      .a�5{      �� 	Setter_10��$	 btnOk��6/   
 � y�$ Visible=�6X   �� 5|      $	 btnOk��6   
 �@��	IsDefault . 6E   �� 5}      	Setter_11��$	 btnYes��6/   
 � y�$ 	Collapsed=�6Y   �� 5~      $	 btnYes��6   
 �@��	IsDefault .  6F   �� 5      	Setter_12��$	 btnNo��6/   
 � y�$ 	Collapsed=�6X   �� 5�      $	 btnNo��6   
 �@��	IsDefault .  6E   �� 5�      	Setter_13��$	 	btnCancel��6/   
 � y�$ 	Collapsed=�6\   �� 5�      $	 	btnCancel��6   
 �@��	IsDefault .  6I   5�      �� 5�      DataTrigger_11�� 65   �� *	Buttons+ 5�       5�      MessageBoxButton_2��OKCancel=�5�      .a�5�      �� 	Setter_14��$	 btnOk��6/   
 � y�$ Visible=�6X   �� 5�      $	 btnOk��6   
 �@��	IsDefault . 6E   �� 5�      	Setter_15��$	 btnYes��6/   
 � y�$ 	Collapsed=�6Y   �� 5�      $	 btnYes��6   
 �@��	IsDefault .  6F   �� 5�      	Setter_16��$	 btnNo��6/   
 � y�$ 	Collapsed=�6X   �� 5�      $	 btnNo��6   
 �@��	IsDefault .  6E   �� 5�      	Setter_17��$	 	btnCancel��6/   
 � y�$ Visible=�6\   �� 5�      $	 	btnCancel��6   
 �@��	IsDefault .  6I   �� 5�      $	 	btnCancel��6   
 �@��IsCancel . 6H   5�      �� 5�      DataTrigger_12�� 65   �� *	Buttons+ 5�       5�      MessageBoxButton_3��	YesNo=�5�      .a�5�      �� 	Setter_18��$	 btnOk��6/   
 � y�$ 	Collapsed=�6X   �� 5�      $	 btnOk��6   
 �@��	IsDefault .  6E   �� 5�      	Setter_19��$	 btnYes��6/   
 � y�$ Visible=�6Y   �� 5�      $	 btnYes��6   
 �@��	IsDefault . 6F   �� 5�      	Setter_20��$	 btnNo��6/   
 � y�$ Visible=�6X   �� 5�      $	 btnNo��6   
 �@��	IsDefault .  6E   �� 5�      	Setter_21��$	 	btnCancel��6/   
 � y�$ 	Collapsed=�6\   �� 5�      $	 	btnCancel��6   
 �@��	IsDefault .  6I   �� 5�      $	 	btnCancel��6   
 �@��IsCancel .  6H   5�      �� 5�      DataTrigger_13�� 65   �� *	Buttons+ 5�       5�      MessageBoxButton_4��YesNoCancel=�5�      .a�5�      �� 	Setter_22��$	 btnOk��6/   
 � y�$ 	Collapsed=�6X   �� 5�      $	 btnOk��6   
 �@��	IsDefault .  6E   �� 5�      	Setter_23��$	 btnYes��6/   
 � y�$ Visible=�6Y   �� 5�      $	 btnYes��6   
 �@��	IsDefault . 6F   �� 5�      	Setter_24��$	 btnNo��6/   
 � y�$ Visible=�6X   �� 5�      $	 btnNo��6   
 �@��	IsDefault .  6E   �� 5�      	Setter_25��$	 	btnCancel��6/   
 � y�$ Visible=�6\   �� 5�      $	 	btnCancel��6   
 �@��	IsDefault .  6I   �� 5�      $	 	btnCancel��6   
 �@��IsCancel . 6H   5�      5�      5�      .��5�      � 
Grid_1�� 5�   
   �� 5�      ColumnDefinition_2��$��80 �6:   �� 5�      ColumnDefinition_3��$��* �6:    5�   
   �� 5�      RowDefinition_2��$
��Auto �64   �� 5�      RowDefinition_3��$��* �64   .+�5�   
   �� -   5        $��imgDlgTypeCtrl��5�   0   imgDlgTypeCtrl��$��0��6F   $��0��6V   $��Center=�6c   $��Center=�6~   #��[  6�   �� 5�   
   -   5        $��
txtMessage��5�   '   
txtMessage��$��Center=�69   $
��Left=�6T   $��0��6o   $��1��6|   $��20,0,5,0q�6�   �� 5�   
   -   5        $��btnCtrl��5�   )   btnCtrl��$��1��68   $��1��6E   $��Right=�6U   $��Bottom=�6q   #��[ 6�   $��Center=�6�   5�      5�      