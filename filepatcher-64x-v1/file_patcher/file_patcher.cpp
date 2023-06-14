
#include <iostream>
#include <fstream>
#include <string>
#include <vector>

// Function to apply the patch to the target file
void applyPatch(const std::string& targetFilePath, const std::string& patchFilePath) {
    // Open the target file and the patch file in binary mode
    std::ifstream targetFile(targetFilePath, std::ios::binary);
    std::ifstream patchFile(patchFilePath, std::ios::binary);

    // Check if both files were opened successfully
    if (!targetFile || !patchFile) {
        std::cerr << "Error opening files!" << std::endl;
        return;
    }

    // Read the content of the patch file into a vector
    std::vector<char> patchData(std::istreambuf_iterator<char>(patchFile), {});

    // Open the target file with read and write capabilities
    std::ofstream patchedFile(targetFilePath, std::ios::binary | std::ios::out | std::ios::in);

    // Check if the target file was opened successfully
    if (!patchedFile) {
        std::cerr << "Error opening target file for writing!" << std::endl;
        targetFile.close();
        patchFile.close();
        return;
    }

    // Determine the size of the target file
    patchedFile.seekp(0, std::ios::end);
    std::streampos fileSize = patchedFile.tellp();
    patchedFile.seekp(0, std::ios::beg);

    // Check if the target file is large enough to accommodate the patch
    if (patchedFile && fileSize >= patchData.size()) {
        // Apply the patch by overwriting the target file content with the patch data
        patchedFile.write(patchData.data(), patchData.size());
        std::cout << "File patched successfully!" << std::endl;
    }
    else {
        //
        std::cerr << "Error applying patch!" << std::endl;
    }

    // Close all file streams
    targetFile.close();
    patchFile.close();
    patchedFile.close();
}

int main() {

    // Define 2 strings that will container target and patch file paths
    std::string targetFilePath;
    std::string patchFilePath;

    // Prompt the user to enter the target file path
    std::cout << "Enter the directory and filename of the file to patch (e.g., /path/to/file.txt): ";
    std::getline(std::cin, targetFilePath);

    // Prompt the user to enter the patch file path
    std::cout << "Enter the directory and filename of the patch file: ";
    std::getline(std::cin, patchFilePath);

    // Call the applyPatch function with the provided file paths
    applyPatch(targetFilePath, patchFilePath);

    return 0;
}


