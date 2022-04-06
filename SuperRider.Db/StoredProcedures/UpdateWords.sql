CREATE PROCEDURE [dbo].[UpdateWords]
	 @tblWords_Count Words_Count_Type readonly

as
begin tran
  merge dbo.Words_Count as target
  using @tblWords_Count as source
    on (target.Word = source.Word)
  when matched then
    update set
      [Count] = target.[Count] + source.[Count]
  when not matched then
    insert(Word, [Count]) 
    values(source.Word, source.[Count]);
commit tran
