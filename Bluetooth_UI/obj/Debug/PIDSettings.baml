   M S B A M L   `   `   `  ����G  CBluetooth_UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Bluetooth_UIBluetooth_UI  5   	       Bluetooth_UI.PIDSettings   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,localclr-namespace:Bluetooth_UI     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ PIDSettings��5   	   $	��200��6   $	��300��6*   .��5      � $��15q�6    � ColumnDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
      Bluetooth_UI.ValueSetter  -   5          Name$ 	vsParamKc��5   )   $��0��6   6>     5   
   -   5        $ 	vsParamTi��5   )   $��1��6   6>     5   
   -   5        $ 	vsParamTd��5   )   $��2��6   6>   � 5   
   $��3��6   $��2��6!   $��Right=�65   $��0,15,0,0q�6Q   .+�5      �� -   5        $��btnDialogOk��5   &    �� 	IsDefault . $��60��6S   $��0,0,10,0q�6a   .��6s   Ok6w   �� 5       �� IsCancel . $��60��6%   .��63   Cancel6;   5      5      5      