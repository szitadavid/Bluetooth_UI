   M S B A M L   `   `   `  ����  Bluetooth_UI    Bluetooth_UI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Ventilator settings��5   	   $	��480��6%   $	��750��62   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $	��145 �6   �� 5	      $	��145 �6   �� 5
      $	��145 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $	��200 �6   �� 5       � 	Resources 5   
      refresh��%3   &	        �� 5      $��Res/refresh.png��6$   5      .+�5   
   �� $��0,5,5,5q�6   $��0��6"   $��0��6/   
�������
�������$��3q�5   8   ._�5      �� .+�5      �� -   5        $��80��5      $��5q�6)   .@�5      �� -   5        $��tabBluetooth��5      ��5      �� 5       �� Orientation$ 
Horizontal=�6)   .+�5   "   �� $��Res/Bluetooth.png��6)   $��15��6D   �� 5    "   $��2,0,0,0q�6,   $��	Bluetooth��6=   5!      .��5#      ]� ��.  .+�5$      �� $
��Left=�6(   $��5,10,0,0q�6>   $
��Left=�6P   Port:6r   � 5%      $
��Left=�6(   $��Right=�6C   .+�5&   "   �� -   5        $��20��5&   )   $��20��65   #��[  6@   6c   �� 5'   "   -   5        $��cbPorts��5'   +   $	��100��6;   $��	5,10,5,10q�6G   $��20��5(   +   $	��Top=�67   5)   #   5*      5+      5,      �� 5-      -   5        $��tabWifi��5-      ��5.      �� 5/      $ 
Horizontal=�6)   .+�50   "   �� -   5         ��Name$ image��50   )   $��15��68   �� 51   "   $��5,0,0,0q�6,   $
��Wifi��6=   52      .��54      �� .��6    Not implemented yet!66   55      56      ]� 58      ��.  .+�59      �� -   5        $��lbConnectionState��59   6   $
��Left=�6    $��	15,10,0,0q�6O   $
��Left=�6b   Connection: �� 5:   &   
���  ��.�6<   down6B   5;      �� 5<      -	   5        $��	btConnect��5<      $��Right=�6.   $	��100��6E   $��	0,10,15,0q�6Q   $
��Left=�6d   $��Connect��5=   /   5>      5?      5@      5A      �� 5D   
   $��0,5,5,5q�6   $��0��6"   $��1��6/   
�������
�������$��3q�5E   8   ._�5F      �� .+�5G      �� $
��Bold"�6   .��6*   Send command to:6<   	� 5H      -
   5        $��rbDestinyBluetooth��5H       	� 	GroupName$ rbGroupDestiny��68   $��5,0,0,0q�5I      .��6T   Bluetooth module6f   	� 5J      -   5        $��rbDestinyWifi��5J      $ rbGroupDestiny��63   $��5,5,0,0q�5K      .��6T   WiFi module6a   	� 5L      -   5        $��rbDestinySystem��5L      $ rbGroupDestiny��65   $��5,5,0,0q�5M      .��6K   System6S   5N      5O      �� 5R   
   $��1��6   $��0��6   $��2��6/   $��0,5,5,5q�5S      
�������
�������$��3q�6M   ._�5T      �� $ 
Horizontal=�6   .+�5U      �� .+�5V      �� .��6   
Command:6&   �� 5W      -   5        $��
cbCommands��5W       $	��140��62   $��5q�6>   $��Right=�6I   5Y      �� 5Z      -   5        $	��100��5Z      $��5q�6)   $��Right=�64   .��5[   0   Send66   5\      �� 5]      $��	100,0,0,0q�6   $ Vertical=�60   .+�5^      �� .��6   Parameters:6)   �� 5_      -   5        $��tbParam1��5_      $	��100��6.   $��5q�6:   {�.  6E   �� 5`      -   5        $��tbParam2��5`      $	��100��6.   $��5q�6:   {�.  6E   �� 5a      -   5        $��tbParam3��5a      $	��100��6.   $��5q�6:   {�.  6E   5b      5c      5d      �� 5g   
   $��0,5,5,5q�6   $��2��6"   $��0��6/   $��2��6?   
�������
�������$��3q�5h   8   ._�5i      �� .+�5j      � .+�5k      �� $
��Bold"�6   .��6.   System settings:6@   �� 5l      -   5        $��btPIDSettings��5l      $��15,0,0,0q�6N   $��20��6`   ��5m      �� 5n      $ 
Horizontal=�6)   .+�5o   "   �� $��Res/Settings.png��6)   $��15��6C   �� 5p   "   $��2,0,0,0q�6,   $	��PID��6=   5q      5s      5t      �� 5u      $��5,0,0,0q�6   .��6,   	Get speed67   �� 5v      $��5,0,0,0q�6   .��6-   Get distance6;   � 5w      .+�5x      �� .��6   
Duty cycle6(   �� 5y      $��0,0,5,0q�6   $��30��6/   $��20��6:   ��6F   ��  �� ElementName$ slValue�� �� Path$ Value�	 �� UpdateSourceTrigger$	 PropertyChanged=�5z   A   �� 5{      -   5        $��slValue��5|   J   
 � Maximum$	
 900Q�5{       � Minimum$	 600Q�6+    �� TickPlacement$ BottomRight=�69    �� TickFrequency$ 1Q�5|       �� IsSnapToTickEnabled . $	��240��6Y   5}      �� 5~      -   5        $��cbSaveToFile��5~   ,   $��5,0,0,0q�6   .��6�   Save to file6�   � 5      .+�5�      �� -   5        $��
tbFilepath��5�       $��5,0,5,0q�62   $	��200��6C   $��20��6R   $��CharacterEllipsis=�5�       � ToolTip 6A   �� *Text+$ 
tbFilepath��6u   �� 5�      -   5        $��btSelectFile��5�      $��25��61   $��20��6<   .��6c   ...6h   5�      5�      5�      �� 5�   
   -   5        $��tbOutput��5�      $��5q�6"   $��0��5�      $��2��6   $��4��6/   " �� VerticalScrollBarVisibility$
 Auto=�6@    �� 
IsReadOnly .  �� TextWrapping$ WrapWithOverflow=�6u   5�      5�      