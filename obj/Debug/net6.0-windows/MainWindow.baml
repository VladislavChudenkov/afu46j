   M S B A M L   `   `   `  ����  
AbobaMusic    AbobaMusic.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Audio Player��5   	   $	��450��6   $	��600��6+   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��0��5      $��0��6   $��Browse��6I   �� 5   
   $��1��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   .+�5      �� -   5        $��Previous��5   7   �� 5      -   5        $��
Play/Pause��5   3   �� 5      -   5        $
��Next��5   3   �� 5      -   5        $��Repeat��5   0   �� 5      -   5        $��Shuffle��5   1   5       �� 5#   
   -   5         ��Name$ positionSlider��5#   y   $��2��6   $��0��6   $��2��6.    � Minimum$ 0Q�6B   6y   �� 5$   
   -   5        $ volumeSlider��5$   �   $��3��6   $��0��6   $��2��6.   $ 0Q�6B    � Maximum$ 1Q�6N   6�   �� 5'   
   $��4��6   $��0��6!   $��Current Position:��61   $��Center=�6J   �� 5(   
   -	   5         ��Name$ currentPositionLabel��5(   1   $��4��6   $��1��6!   $��Center=�6O   �� 5*   
   $��5��6   $��0��6!   $��Remaining Time:��61   $��Center=�6H   �� 5+   
   -
   5        $ remainingTimeLabel��5+   1   $��5��6   $��1��6!   $��Center=�6M   r� 5.   
   -   5         r�Name$ mediaPlayer��5.   E   $��0��6   $��6��6$   $��1��65   	 r� LoadedBehavior$	 Manual=�6�   
 r� UnloadedBehavior$
 Manual=�6�   50      51      