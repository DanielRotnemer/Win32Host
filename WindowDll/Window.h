#pragma once

/* Disable rarely used windows stuff */
#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif // !WIN32_LEAN_AND_MEAN

#include <Windows.h>

/* Window procedure */
LRESULT CALLBACK window_proc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam)
{
	switch (msg)
	{
		case WM_CLOSE: // When the window is closed
			DestroyWindow(hwnd);
			return 0;
		case WM_DESTROY: // When the window is destoryed
			PostQuitMessage(0);
			return 0;
		case WM_SIZE: // When the window is resized
			break;
		default:
			break;
	}
	// return the default window procedure
	return DefWindowProc(hwnd, msg, wparam, lparam);
}

#define DLL_EXPORT extern "C" __declspec(dllexport)
using window_handle = HWND;

window_handle window{ nullptr };

DLL_EXPORT window_handle InitializeWin32(window_handle parent, int width, int height)
{
	/* Create window class */
	WNDCLASSEX wc;
	ZeroMemory(&wc, sizeof(wc));
	wc.cbSize = sizeof(wc);
	wc.style = CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc = window_proc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hInstance = 0;
	wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.hbrBackground = CreateSolidBrush(RGB(65, 65, 65)); // Red Background color
	wc.lpszMenuName = NULL;
	wc.lpszClassName = L"Win32Window";
	wc.hIconSm = LoadIcon(NULL, IDI_APPLICATION);

	/* Register window class */
	RegisterClassEx(&wc);
	RECT rect{};
	GetWindowRect(parent, &rect);

	/* Adjust window style to current devie size */
	AdjustWindowRect(&rect, WS_CHILD, FALSE);

	const int top{ rect.top };
	const int left{ rect.left };
	const int init_width{ rect.right - left };
	const int init_height{ rect.bottom - top };

	const wchar_t* caption{ L"Win32 window caption" };

	/* Create an instance of the window class */
	window = CreateWindowEx(
		0,						// extended style
		wc.lpszClassName,		// window class name
		caption,				// instance title
		WS_CHILD,				// window style
		left,					// initial window left position
		top,					// initial window top position
		init_width,				// initial window width
		init_height,			// initial window height
		parent,					// handle to parent window
		NULL,					// handle to menu
		NULL,					// instance of this application
		NULL					// extra creation parameters
	);

	ShowWindow(window, SW_SHOWNORMAL);
	UpdateWindow(window);
	return window;
}
