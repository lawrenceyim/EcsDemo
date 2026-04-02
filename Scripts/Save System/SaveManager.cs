using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaveSystem;

public class SaveManager {
    public List<ISaveAndLoad> _saveAndLoads = [];

    public void SaveGameData() {
        Task.Run(() => {
            // Saving using copy of game objects
        });
    }
}