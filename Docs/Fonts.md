**Using local fonts in the application**

1. Local fonts properties must be set to:

   1. Build Action => Resource
   2. Copy to Output Directory => Do not copy

2. Defined key in the **Fonts.xaml**

   ```XML
   <FontFamily x:Key="SegMDL2">pack://application;,,,/Fonts/#Segoe MDL2 Assets</FontFamily>
   ```

