   M S B A M L   `   `   `  ����K  GSalaryCalculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SalaryCalculatorSalaryCalculator  5   	   (    "SalaryCalculator.AboutEmployeeForm   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SalaryCalculator     5   	     � Title$  About��5   	   $	��450��6   $	��800��6'    � WindowStartupLocation$ CenterScreen=�66   .��5	      �  � ShowGridLines$
 True��6    � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $
��0,2* �6   6,   �� 5      $
��0,2* �6   6,   �� 5      $
��0,6* �6   6,   �� 5      $
��0,2* �6   6,   .+�5   
   �� -   5         ��Name$ fullNameBlock��5      $��3��6+   $��0��6?   %#Тут должна быть ФИО6a   �� 5   
   $��0��6   $��1��6$   ;9Выберите диапазон дат операций6Q   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ datePicker1��5      $��1��6*   $��1��6:      SelectedDateFormat$ Short=�6G   6d     5   
   -   5        $ datePicker2��5      $��2��6*   $��1��6:   6x   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ operationsGrid��5      	   AutoGenerateColumns$	 False��6+   
   
IsReadOnly$

 True��6G   $��3��6Z   $��2��6n      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ id��6/   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   ��  �� Path$ op_id�6l     5      $ * 6%   $ Название��6/    6K   �� $ op_title�6o     5       $ * 6%   $ Дата��6/    6K   �� $ op_date�6n     5!      $ * 6%   $( "Продолжительность��6/    6K   �� $ op_hours�6o     5"      $ * 6%   $ Ставка��6/    6K   �� $ op_rate�6n     5#      $ * 6%   $ 
Сумма��6/    6K   �� $ op_sum�6m   5%      �� 5'   
   -   5        $'��!Добавить операцию��5'      $��0��6-   $��3��6=   $��10q�6k   6y   �� 5(   
   -   5        $%��Удалить операцию��5(      $��1��6,   $��3��6<   $��10q�6I   6{   �� 5)   
   -   5        $��Вернутся��5)      $��2��6$   $��3��64   $��10q�6[   6i   5*      5+      