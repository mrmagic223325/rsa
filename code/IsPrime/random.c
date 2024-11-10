#include <stdio.h>
#include <stdlib.h>
#include <sys/random.h>


void _random(void *buf, size_t buflen);
int *get_random_bytes(int size);

void main(void)
{
  printf("%d", *((int*)get_random_bytes(1)));
}

int *get_random_bytes(int size)
{
  void *buf = malloc(size);
  _random(buf, size);
  return buf;
}

void _random(void *buf, size_t buflen)
{
  getrandom(buf, buflen, GRND_NONBLOCK);
}
