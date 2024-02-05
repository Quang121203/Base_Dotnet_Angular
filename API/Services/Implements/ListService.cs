using API.DataAccess;
using API.Models.Domains;
using API.Services.Interfaces;

namespace API.Services.Implements
{
    public class ListService : IListService
    {
        private readonly IUnitOfWork unitOfWork;
        public ListService(IUnitOfWork unitOfWork) { 
            this.unitOfWork = unitOfWork;
        }

        public async Task<object> CreateList(List model)
        {
            await unitOfWork.ListRepository.InsertAsync(model);
            await unitOfWork.SaveChangesAsync();
 
            return (new
            {
                EM = 0,
                EC = "create successfully",
                DT = ""
            });
        }

        public async Task<object?> DeleteList(int id)
        {
            var list = await unitOfWork.ListRepository.GetSingleAsync(id);
            if (list != null)
            {
                var delete = await unitOfWork.ListRepository.DeleteAsync(list.Id);
                await unitOfWork.SaveChangesAsync();
                if (delete)
                {
                    return (new
                    {
                        EM = "delete successfully",
                        EC = 0,
                        DT = ""
                    });
                }
                return null;
            }
            return (new
            {
                EM = "not found this list",
                EC = 1,
                DT = ""
            });
        }

        public async Task<object> GetList(string genre, string type )
        {
            if (type!="null")
            {
                if (genre!= "null")
                {
                    return (new
                    {
                        EC = 0,
                        EM = "",
                        DT = await unitOfWork.ListRepository.GetAsync(x => (x.Type == genre && x.Genre==genre))
                    });
                }

                return (new
                {
                    EC = 0,
                    EM = "",
                    DT = await unitOfWork.ListRepository.GetAsync(x => x.Type == genre)
                }) ;
            }

            return (new
            {
                EC = 0,
                EM="",
                DT= await unitOfWork.ListRepository.GetAsync()
            });
        }
    }
}
