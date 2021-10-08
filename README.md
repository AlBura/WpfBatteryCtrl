# WpfBatteryCtrl
WPF C# library with some batteries user controls 

## Introduction

Simple library to show the status of batteries using WPF and C#

## Usage

- Include Namespace
```xml
<Window ...
         xmlns:batCtrl="clr-namespace:WpfBatteryCtrl;assembly=WpfBatteryCtrl" 
```
- Add Loading indicator and select mode
```xml
  <batCtrl:BatteryCtrlSingleLine   Value="50" ></batCtrl:BatteryCtrlSingleLine>
    
```

## Features
- Vertical/Horizontal orientation
- Configurable number ticks
- Changing color base thresholds 