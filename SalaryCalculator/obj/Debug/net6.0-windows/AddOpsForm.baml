   M S B A M L   `   `   `  ����K  GSalaryCalculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SalaryCalculatorSalaryCalculator  5   	   !    SalaryCalculator.AddOpsForm   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SalaryCalculator     5   	     � Title$  Add opertaion��5   	   $	��300��6   $	��400��6/    � SizeToContent$ WidthAndHeight=�6>    � WindowStartupLocation$ CenterScreen=�6]   .��5	      �  � ShowGridLines$
 True��6    � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��0��6   #!Содержание работы64   �� 5   
   $��1��6   
Дата6'   �� 5   
   $��2��6   !Количество часов63   �� 5   
   $��3��6   Ставка6)   �� 5   
   $��4��6   
Сумма6(   �� 5   
   -   5         ��Name$ TextBox_OpTitle��5      $��1��6+   $��0��6;   .��6H   ��5      �� 5       �� Path$ op_title�6    �� ValidationRules 5       5  /System.Windows.Controls.ExceptionValidationRule  5!      64   5#      .��5$      5%      �� 5&   
   -   5        $ TextBox_OpDate��5&      $��1��6*   $��1��6:   	 � ToolTip$	 format: dd.mm.yyyy��6G   .��6�   ��5'      �� 5(      $ op_date�6   
 �� NotifyOnValidationError$

 True��6)    5)      5  /System.Windows.Controls.DataErrorValidationRule  5*      64   5,      .��5-      5.      �� 5/   
   -   5        $ TextBox_OpHours��5/      $��1��6+   $��2��6;   .��6H   ��50      �� 51      $ op_hours�6    52        53      64   55      .��56      57      �� 58   
   -   5        $ TextBox_OpRate��58      $��1��6*   $��3��6:   .��6G   ��59      �� 5:      $ op_rate�6    5;        5<      64   5>      .��5?      5@      �� 5A   
   -   5         ��Name$ TextBox_OpSumm��5A      $��1��6,   $��4��6<   5B      � 5D   
   $��2��6   $��5��6(   $��Center=�65   $��0 10 0 0q�6R   .+�5E      �� -   5         ��Name$ OkButton��5E      $��OK��6'   $��14(�64   $��60��6B   $��0, 0, 20 ,0q�6P    �� 	IsDefault$
 True��6e   $
{�True��6�   6�   �� 5F      -   5        $��Cancel��5F      $��14(�6&   $��60��64    �� IsCancel$
 True��6B   6n   5G      5H      5I      