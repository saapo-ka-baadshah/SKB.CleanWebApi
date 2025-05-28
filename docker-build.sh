#!/usr/bin/env bash

# Attempt to find the Git repository root
GIT_REPO_ROOT=$(git rev-parse --show-toplevel 2>/dev/null)
EXIT_STATUS=$? # Capture the exit status of the git command

if [ $EXIT_STATUS -ne 0 ]; then
  echo "Error: Not currently in a Git repository, or 'git' command is not available."
  exit 1
fi

ENV_FILE_PATH="$GIT_REPO_ROOT/.env"

echo "Checking for .env file at repository root: $GIT_REPO_ROOT"

if [ -f "$ENV_FILE_PATH" ]; then
  echo "Found: $ENV_FILE_PATH"
  echo "Environment Found at $ENV_FILE_PATH"
else
  echo "Not found: .env file does not exist at $GIT_REPO_ROOT"
  echo "Please ensure a '.env' file (or a symlink to one) is present at the repository root if required."
  exit 1 # Failure
fi

######## Business Logic

# Auto Exports the variables temporarily
set -a

# shellcheck disable=SC1090
source $ENV_FILE_PATH

# Disable Auto Exports
set +a

if [ -z "${GITHUB_PACKAGES_FEED_PAT}" ] || [ -z "${GITHUB_USERNAME}" ]; then
    echo "Failed to read the '.env' File"
    exit 1
fi

docker buildx build \
	--secret id=ghUser,env=GITHUB_USERNAME \
	--secret id=ghPass,env=GITHUB_PACKAGES_FEED_PAT \
	. \
	"$@"
