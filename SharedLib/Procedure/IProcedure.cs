namespace Rudeus.Procedure
{
    /// <summary>
    /// �ЂƂ܂Ƃ܂�̏��������s����C���^�[�t�F�[�X
    /// �Ⴆ�΁A�A�N�Z�X�g�[�N���̍Ĕ��s�A�C���X�g�[���ς݃A�v���̑��M�Ȃ�
    /// </summary>
    public interface IProcedure
    {
        /// <summary>
        /// ���������s����
        /// </summary>
        Task Run();
    }
}