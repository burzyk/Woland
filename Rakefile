require 'rake'
require 'rest-client'
require 'fileutils'

BUILD_DIR="build"

task :default => [:init, :download_dotnet, :restore_packages] 

task :init do
    puts "Initializing build ..."

    FileUtils.rm_rf(BUILD_DIR) if Dir.exists?(BUILD_DIR)
    Dir.mkdir(BUILD_DIR)
end

task :download_dotnet do 
    puts "downloading .NET ..."
    response = RestClient.get "https://go.microsoft.com/fwlink/?LinkID=809119"
    File.write(BUILD_DIR + "/dotnet.tgz", response.body)
    
    puts "extracting .NET ..."
end

task :restore_packages do
end