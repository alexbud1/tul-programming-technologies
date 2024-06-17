using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

public interface IGenerator
{
    void GenerateUserModels(UserMasterViewModel viewModel);

    void GenerateBookModels(BookMasterViewModel viewModel);

    void GenerateStateModels(StateMasterViewModel viewModel);

    void GenerateEventModels(EventMasterViewModel viewModel);
}
