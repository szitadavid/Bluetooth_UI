   M S B A M L   `   `   `  ����   Bluetooth_UI    Bluetooth_UI.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Ventilator settings��$	��600��$	��472�� � 	Resources    BluetoothImage �� 	UriSource��%A   &	        �� $1 +/Bluetooth_UI;component/Media/Bluetooth.png?�.��� $��5q� � RowDefinitions �� $	��145 ��� $	��145 ��� $	��145 ���  � ColumnDefinitions �� $	��250 ��� $	��200 ���  ��%A   &	        �� $1 +/Bluetooth_UI;component/Media/Bluetooth.png?�.+��� $��0,5,5,5q�$��0��$��0��
�������
�������$��3q�._��� .+��� -   $��80��$��5q�.@��� -   $��tabBluetooth������  �� Orientation$ 
Horizontal=�.+��� #��[  $��2��$��2���� $1��+/Bluetooth_UI;component/Media/Bluetooth.jpg��$��2��$��2���� $��	Bluetooth��.��]� ��.  .+��� $
��Left=�$��5,10,0,0q�$
��Left=�Port:�� -   $��cbPorts��$��Right=�$	��100��$��	5,10,5,10q�$
��Left=�$��20��$	��Top=��� -   $��tabWifi��$
��WiFi��.���� .��Not implemented yet!]� ��.  .+��� -   $��lbConnectionState��$
��Left=�$��	15,10,0,0q�$
��Left=�Connection: �� 
���  ��.�down�� -   $��	btConnect��$��Right=�$	��100��$��	0,10,15,0q�$
��Left=�$��Connect���� $��0,5,5,5q�$��0��$��1��
�������
�������$��3q�._��� .+��� $
��Bold"�.��Send command to:	� -   $��rbDestinyBluetooth�� 	� 	GroupName$ rbGroupDestiny��$��5,0,0,0q�.��Bluetooth module	� -	   $��rbDestinyWifi��$ rbGroupDestiny��$��5,5,0,0q�.��WiFi module	� -
   $��rbDestinySystem��$ rbGroupDestiny��$��5,5,0,0q�.��System�� $��1��$��0��$��2��$��0,5,5,5q�
�������
�������$��3q�._��� $ 
Horizontal=�.+��� .+��� .��
Command:�� -   $��
cbCommands��$	��140��$��5q�$��Right=��� -   $	��100��$��5q�$��Right=�.��Send�� $��	100,0,0,0q�$ Vertical=�.+��� .��Parameters:�� -   $��tbParam1��$	��100��$��5q�{�.  �� -   $��tbParam2��$	��100��$��5q�{�.  �� -   $��tbParam3��$	��100��$��5q�{�.  �� $��0,5,5,5q�$��2��$��0��$��2��
�������
�������$��3q�._��� .+��� $
��Bold"�.��System settings:�� $��5,0,0,0q�.��	Get speed�� $��5,0,0,0q�.��Get distance� .+��� .��
Duty cycle�� $��0,0,5,0q�$��30��$��20������  �� ElementName$ slValue�� �� Path$ Value�	 �� UpdateSourceTrigger$	 PropertyChanged=��� -   $��slValue��
 � Maximum$	
 900Q� � Minimum$	 600Q� �� TickPlacement$ BottomRight=� �� TickFrequency$ 1Q� �� IsSnapToTickEnabled . $	��240���� -   $��cbSaveToFile��$��5,0,0,0q�.��Save to file� .+��� -   $��
tbFilepath��$��5,0,5,0q�$	��200��$��20��$��CharacterEllipsis=� � ToolTip �� *Text+$ 
tbFilepath���� -   $��btSelectFile��$��25��$��20��.��...�� -   $��tbOutput��$��5q�$��3��$��0��$��2��" �� VerticalScrollBarVisibility$
 Auto=� �� 
IsReadOnly .  �� TextWrapping$ WrapWithOverflow=�