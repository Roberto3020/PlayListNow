FROM jenkins/jenkins:lts

USER root

# Instalar docker cli
RUN apt-get update && apt-get install -y \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg-agent \
    software-properties-common \
    lsb-release

RUN curl -fsSL https://download.docker.com/linux/debian/gpg | apt-key add -

RUN add-apt-repository \
   "deb [arch=amd64] https://download.docker.com/linux/debian \
   $(lsb_release -cs) \
   stable"

# Instalar Docker CLI manualmente
RUN curl -fsSL https://download.docker.com/linux/static/stable/x86_64/docker-24.0.2.tgz -o docker.tgz \
    && tar --strip-components=1 -xvzf docker.tgz -C /usr/local/bin docker/docker \
    && rm docker.tgz

# Instalar docker-compose
RUN curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose \
    && chmod +x /usr/local/bin/docker-compose

# Permisos para usar docker socket
RUN usermod -aG docker jenkins

USER jenkins
