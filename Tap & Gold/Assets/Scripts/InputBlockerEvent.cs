
/// 이 cs파일은 유저의 모든 입력을 막는 InputBlocker 오브젝트를 관리합니다.
public partial class ClickerTemp
{
    /// InputBlocker 를 활성화시켜 유저의 모든 입력을 막습니다.
    private void BlockUserInput()
    {
        if(!Execute_BlockUserInput) return;
        Execute_BlockUserInput = false;
        inputBlocker.SetDisplay(true);
    }


    /// InputBlocker 를 비활성화시켜 유저의 모든 입력을 허용합니다.
    private void AllowUserInput()
    {
        if(!Execute_AllowUserInput) return;
        Execute_AllowUserInput = false;
        inputBlocker.SetDisplay(false);
    }
}