require 'rake'
require 'fileutils'
require 'open-uri'
require 'zlib'
require 'rubygems/package'
require 'rubygems/package'

CONFIGURATION="Release"
RUNTIME="osx.10.11-x64"
BUILD_DIR="build"
DOTNET_DOWNLOAD_URL="https://go.microsoft.com/fwlink/?LinkID=809128"
#osx "https://go.microsoft.com/fwlink/?LinkID=809128"
#deb "https://go.microsoft.com/fwlink/?LinkID=809119"
DOTNET_TAR_FILE=BUILD_DIR + "/dotnet.tar"
DOTNET_EXE=BUILD_DIR + "/dotnet"
APPLICATION_OUTPUT=BUILD_DIR + "/app"

task :default => [:download_dotnet, :build] 

task :init do
    puts "Initializing build ..."

    FileUtils.rm_rf(BUILD_DIR) if Dir.exists?(BUILD_DIR)
    Dir.mkdir(BUILD_DIR)
end

task :download_dotnet do 
    puts "downloading .NET ..."

    unless File.exists?(DOTNET_TAR_FILE) 
        source = open(DOTNET_DOWNLOAD_URL)
        gz = Zlib::GzipReader.new(source) 
        result = gz.read
        File.write(DOTNET_TAR_FILE, result)
    end

    puts "extracting .NET ..."

    File.open(DOTNET_TAR_FILE, "r") do |f|
        Gem::Package::TarReader.new(f) do |tar|
            tar.each do |entry| 
                path = BUILD_DIR + entry.full_name[1, 256]                
                if entry.directory?()
                    Dir.mkdir(path) if !Dir.exists?(path)
                else 
                    File.write(path, entry.read())
                end
            end
        end
    end
    
    FileUtils.chmod("a+x", DOTNET_EXE)
end

task :build do
    puts "Building application ..."

    run_cmd(DOTNET_EXE + " restore")
    run_cmd(DOTNET_EXE + " test test/Woland.Tests")
    run_cmd(DOTNET_EXE + " publish src/Woland.Service --runtime #{RUNTIME} --configuration #{CONFIGURATION}")

    puts "Getting application ..."
    FileUtils.copy_entry("src/Woland.Service/bin/#{CONFIGURATION}/netcoreapp1.0/#{RUNTIME}/publish", APPLICATION_OUTPUT)

    puts "Getting config file ..."
    FileUtils.cp("src/Woland.Service/config.json", APPLICATION_OUTPUT)
end

def run_cmd(cmd)
    puts %x{ #{cmd} }

    if $?.exitstatus != 0
        raise cmd + " exited with non zero code"
    end
end
