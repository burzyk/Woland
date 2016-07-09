FROM microsoft/dotnet:1.0.0-core-deps

RUN mkdir /opt/woland

COPY build/app /opt/woland