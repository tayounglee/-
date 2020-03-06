#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <Windows.h>


char mine[100][100]={""};
char map[100][100]={"c"};

void print(int x, int y);

int main(){
	int a, b, i;
	int x = 0, y = 0, mine_max = 0, n = 0;
	srand((unsigned)time('\0'));

	while(1){
		printf("Áö·ÚÆÇ Å©±â ÀÔ·Â(x <= 20, y <= 20) : \n");
		scanf("%d %d", &x, &y);
  
	    if(x <= 20 && y <= 20)
		{
			break; 
		}
	}

	printf("Áö·Ú °¹¼ö ÀÔ·Â.\n");
	scanf("%d",&mine_max);
 
	for(i = 0; i < mine_max; i++){
		do
		{
			a = rand()%x;
			b = rand()%y;
		}
		while(mine[a][b] == '*');

		mine[a][b] = '*';
	}
	
	for(a = 1;a <= x + 1;a++)
	{
		for(b = 1;b <= y + 1;b++)
		{
			while(mine[a-1][b-1] == '*'){
				
					if(mine[a-2][b-2] != '*'){
						mine[a-2][b-2] = mine[a-2][b-2]+1;
						if(a == 1){
							mine[a-2][b-2] = mine[a-2][b-2]-1;
						}
					}
					if(mine[a-2][b-1] != '*'){
						mine[a-2][b-1] = mine[a-2][b-1]+1;
						if(a == 1){
							mine[a-2][b-1] = mine[a-2][b-1]-1;
						}
					}
					if(mine[a-2][b] != '*'){
						mine[a-2][b] = mine[a-2][b]+1;
						if(b == y || a == 1){
							mine[a-2][b] = mine[a-2][b]-1;
						}
					}
					if(mine[a-1][b-2] != '*'){
						mine[a-1][b-2] = mine[a-1][b-2]+1;
					}
					if(mine[a-1][b] != '*'){
						mine[a-1][b] = mine[a-1][b]+1;
						if(b == y){
							mine[a-1][b] = mine[a-1][b]-1;
						}
					}
					if(mine[a][b-2] != '*'){
						mine[a][b-2] = mine[a][b-2]+1;
						if(a == x){
							mine[a][b-2] = mine[a][b-2]-1;
						}
					}
					if(mine[a][b-1] != '*'){
						mine[a][b-1] = mine[a][b-1]+1;
						if(a == x){
							mine[a][b-1] = mine[a][b-1]-1;
						}
					}
					if(mine[a][b] != '*'){
						mine[a][b] = mine[a][b]+1;
						if(a == x || b == y){
							mine[a][b] = mine[a][b]-1;
						}
					}
					break;
			}
		}
	}
	system("cls");

	for(a = 0;a < x; a++){
		for(b = 0;b < y; b++){
			map[a][b] = 'c';
			if(map[a][b] == 'c'){
				printf("¡á");
			}
		}
		printf("\n");
	}

	while(1){
		printf("ÁÂÇ¥ÀÔ·Â(x, y) : ");
		scanf("%d %d", &a,&b);
	
		system("cls");
		map[a][b] = mine[a][b];
		print(x, y);

		if(map[a][b] == '*'){
			printf("ÆĞ¹èÇÏ¼Ì½À´Ï´Ù.\n");
			exit(1);
		}
	}
	return 0;
}

void print(int x, int y){
	int a, b;

	for(a = 0;a < x; a++){
		for(b = 0;b < y; b++){
			if(map[a][b] == 'c'){
				printf("¡á");
			}
			else if(map[a][b] == '*'){
				printf("%c ", map[a][b]);
			}
			else if(!map[a][b]){
				printf("¡à");
			}
			else{
			printf("%d ", map[a][b]);
			}
		}
		printf("\n");
	}
	printf("\n");
}