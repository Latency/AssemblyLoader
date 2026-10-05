# Copilot Instructions

## Project Guidelines
- Do not generate or create .md (Markdown) files. Avoid creating documentation files with .md extensions.
- Do not create local files relative to the solution directory. Only modify `Directory.Build.props` and `Directory.Build.targets` files found in `D:\Runners\` (which are symlinks to `D:\Source\`).
- Modify `src/AssemblyLoader.csproj` with caution due to the .NET 11 RC SDK's issue where AOT properties are evaluated before any MSBuild files are loaded, during the initial "Determining projects" phase. This occurs even before the first PropertyGroup in any file is processed.
- Set the language version to "Preview" in `Directory.Build.props`, not "latest`.