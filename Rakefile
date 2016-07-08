require 'rake'
require 'fileutils'

RUNTIME="osx.10.11-x64"
BUILD_DIR="build"
DOTNET_EXE="dotnet"
APPLICATION_OUTPUT=BUILD_DIR + "/app"

task :default => [:build_dev, :build_release] 

task :init do
    puts "Initializing build ..."

    FileUtils.rm_rf(BUILD_DIR) if Dir.exists?(BUILD_DIR)
    Dir.mkdir(BUILD_DIR)
end

task :restore_packages do
    puts "Restoring packages ..."
    run_cmd(DOTNET_EXE + " restore")
end

task :tests => :restore_packages do
    puts "Running tests ..."
    run_cmd(DOTNET_EXE + " test test/Woland.Tests")
end

task :build_dev => :restore_packages do
    run_cmd(DOTNET_EXE + " build src/Woland.Service")
    FileUtils.cp("src/Woland.Service/config.json", "src/Woland.Service/bin/Debug/netcoreapp1.0/config.json")
end

task :build_release => [:init, :tests, :restore_packages] do
    configuration = "Release"

    puts "Building application ..."
    run_cmd(DOTNET_EXE + " publish src/Woland.Service --runtime #{RUNTIME} --configuration #{configuration}")

    puts "Getting application ..."
    FileUtils.copy_entry("src/Woland.Service/bin/#{configuration}/netcoreapp1.0/#{RUNTIME}/publish", APPLICATION_OUTPUT)

    puts "Getting config file ..."
    FileUtils.cp("src/Woland.Service/config.json", APPLICATION_OUTPUT)
end

def run_cmd(cmd)
    puts %x{ #{cmd} }

    if $?.exitstatus != 0
        raise cmd + " exited with non zero code"
    end
end