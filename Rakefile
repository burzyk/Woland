require 'rake'
require 'fileutils'

module OS
    def OS.windows?
        (/cygwin|mswin|mingw|bccwin|wince|emx/ =~ RUBY_PLATFORM) != nil
    end

    def OS.mac?
        (/darwin/ =~ RUBY_PLATFORM) != nil
    end

    def OS.linux?
        OS.unix? and not OS.mac?
    end
end

DEBIAN_RUNTIME="debian.8-x64"
WINDOWS_RUNTIME="win10-x64"
OSX_RUNTIME="osx.10.11-x64"

DOCKER_IMAGE_RUNTIME=DEBIAN_RUNTIME
RUNTIME=OS.windows? ? WINDOWS_RUNTIME : OS.mac? ? OSX_RUNTIME : DEBIAN_RUNTIME
BUILD_DIR="build"
DOTNET_EXE="dotnet"
APPLICATION_OUTPUT=BUILD_DIR + "/app"

task :default => [:build_image] 

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
    FileUtils.cp("src/Woland.Service/config.json", "src/Woland.Service/bin/Debug/netcoreapp1.0/#{RUNTIME}/config.json")
end

task :build_release => [:init, :tests, :restore_packages] do
    configuration = "Release"

    puts "Building application ..."
    run_cmd(DOTNET_EXE + " publish src/Woland.Service --runtime #{DOCKER_IMAGE_RUNTIME} --configuration #{configuration}")

    puts "Getting application ..."
    FileUtils.copy_entry("src/Woland.Service/bin/#{configuration}/netcoreapp1.0/#{DOCKER_IMAGE_RUNTIME}/publish", APPLICATION_OUTPUT)

    puts "Getting config file ..."
    FileUtils.cp("src/Woland.Service/config.json", APPLICATION_OUTPUT)
end

task :build_image => :build_release do
    puts "Building image ..."
    run_cmd("docker build -t burzyk/woland .")
    run_cmd("docker login -u burzykci -p $DOCKER_PASSWORD -e p.k.burzynski+burzykci@gmail.com")

    puts "Pushing image to docker hub (this may take a while) ..."
    run_cmd("docker push burzyk/woland")
end

def run_cmd(cmd)
    status = system(cmd)

    if !status
        raise cmd + " exited with non zero code"
    end
end
