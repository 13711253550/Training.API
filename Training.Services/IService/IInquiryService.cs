using Training.Domain.DTO;
using Training.Domain.Entity.UserEntity;
using Training.Domain.Shard;

namespace Training.Services.IService
{
    public interface IInquiryService
    {
        Result<List<Clinical_Reception_DTO>> GetClinical_Reception(int Id);
        Result<List<Doctor>> GetDoctor();
        Result<List<Label>> GetLabel();
        Result<string> AddClinical_Reception(ViewAddClinical viewAddClinical);
        Result<string> DoctorLogin(ViewDoctorLogin viewDoctorLogin);
        Result<string> DeleteClinical_Reception(int Uid);
        Result<string> AddPrescription(ViewAddPrescription viewAddPrescription);
        Result<List<DrugShow_DTO>> DrugShow();
        Result<List<Inquiry_Prescription>> GetInquiry_PrescriptionList(string uid);
        Result<string> AddInquiry_Result(ViewAddInquiry_Result viewAddInquiry_Result);
        Result<object> GetresultList(int uid);
        Result<string> AddDrgOrder(List<ViewDrug> viewDrug);
        Result<object> GetGoodsOrder(int uid);
        ViewAddAlipayTrade GetInquiry(int id);
        void UptState(int id);
    }
}