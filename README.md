# SKB.CleanWebApi 🚀

## Usage
Using the CleanWebApi, we need to first create the
`.env` file which should look something like this:
```bash
GITHUB_USERNAME=YOUR_GITHUB_NAMESPACE
GITHUB_PACKAGES_FEED_PAT=YOUR_GITHUB_PAT_WITH_FEED_RW_PERMISSION
```
Without this the build process fails.

### BUILD
Run the shell script (Keep in mind that is a wrapper over
the actual `docker build` subroutine to add experimental features)

```bash
./docker-build.sh <ALL YOUR DOCKER BUILD ARGS GO HERE>
```

Example:
```bash
./docker-build.sh \
	-t clean-web-api:latest
```

# Execution
Execution is handled by standard `docker run`

## Theory
Refer to the [Theory Information](./README.theory.md)
