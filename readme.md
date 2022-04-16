### 键盘记录器

这是一个包装成我也不知道是什么游戏脚本的键盘记录器

以下需要注意:

- 你可以通过更改`SubForm.cs`以修改界面

- 而`HookTestForm.cs`里的`keyboardHook_KeyDown`方法实现了按下某个键执行某个脚本

- `SimulationKeyboardInput`方法中在执行完应该输入的按键后还会再次执行一次回车，如不需要可以删除
- `EmailClient.cs`实现了发送邮件行为，并且在`HookTestForm.cs`与`SubForm.cs`均有关于向指定邮箱发送信息的方法，如不需要请注意删改，如果需要请修改`EmailClient.cs`中的*收件人邮箱*、*发件人邮箱*和*发件人密码*，否则会造成程序关闭时假死
- `MouseKeyboardLibrary`项目可以卸载，并且`HookSampleApplication`不会引用这个项目，他们已经被封装在了一起，`MouseKeyboardLibrary`项目存在的意义是了解监听钩子的实现方式



### 已知问题

- 有卡死概率
- 注释较少
- 结构混乱
- 仅有通过邮件报告按键信息