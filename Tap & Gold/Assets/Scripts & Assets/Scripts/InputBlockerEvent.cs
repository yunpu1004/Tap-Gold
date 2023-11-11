public partial class ClickerTemp
{
    /// === 실행 내용 ===
    /// 1. InputBlocker 를 활성화시켜 유저의 모든 입력을 막는다
    private void BlockUserInput()
    {
        if(!Execute_BlockUserInput) return;
        Execute_BlockUserInput = false;
        inputBlocker.SetDisplay(true);
    }

    /// === 실행 내용 ===
    /// 1. InputBlocker 를 비활성화시켜 유저의 모든 입력을 허용한다
    private void AllowUserInput()
    {
        if(!Execute_AllowUserInput) return;
        Execute_AllowUserInput = false;
        inputBlocker.SetDisplay(false);
    }
}