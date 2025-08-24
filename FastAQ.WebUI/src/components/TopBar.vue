<template>
  <header class="topbar" role="banner">
    <nav class="topbar-inner" aria-label="Top navigation">
      <!-- 品牌 -->
      <a class="brand" href="/" aria-label="FastAQ Home">
        <h1 class="brand-text">
          Fast<strong>A</strong>&<strong>Q</strong>
        </h1>
      </a>

      <!-- 操作按钮 -->
      <div class="actions">
        <!-- GitHub -->
        <a
          class="icon-btn"
          href="https://github.com/Lixeer/FastAQ"
          target="_blank"
          rel="noopener noreferrer"
          aria-label="Open GitHub"
          data-tooltip="Github Repository"
        >
          <i class="fa-brands fa-github"></i>
        </a>

        <!-- Bilibili -->
        <a
          class="icon-btn"
          href="https://www.bilibili.com"
          target="_blank"
          rel="noopener noreferrer"
          aria-label="Go to post A&Q"
          data-tooltip="mail to us"
        >
          <i class="fa-solid fa-envelope"></i>
        </a>
      </div>
    </nav>
  </header>
</template>

<script setup lang="ts">
defineOptions({ name: "TopBar" })
</script>

<style scoped>
@import url("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css");

/* 顶栏背景 */
.topbar {
  position: sticky;
  top: 0;
  z-index: 50;
  padding: 10px 20px;
  background: rgba(255, 255, 255, 0.85);
  -webkit-backdrop-filter: blur(12px);
  backdrop-filter: blur(12px);
  border-bottom: 1px solid rgba(0, 0, 0, 0.08);
}

/* 内部布局 */
.topbar-inner {
  max-width: 1180px;
  margin: 0 auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

/* 品牌 */
.brand {
  text-decoration: none;
  transition: transform 0.2s ease;
}

.brand:hover {
  transform: scale(1.05);
}

.brand-text {
  margin: 0;
  font-size: clamp(20px, 3.2vw, 26px);
  font-weight: 700;
  font-family: 'Inter', 'Segoe UI', Roboto, sans-serif;
  letter-spacing: -0.5px;
  color: #0f172a;
}

.brand-text strong {
  color: #0284c7; /* 高亮 AQ */
}

/* 操作按钮组 */
.actions {
  display: flex;
  align-items: center;
  gap: 12px;
}

/* 图标按钮 */
.icon-btn {
  position: relative;
  display: grid;
  place-items: center;
  width: 38px;
  height: 38px;
  border-radius: 10px;
  background: rgba(248, 250, 252, 0.85);
  border: 1px solid #e2e8f0;
  color: #0f172a;
  text-decoration: none;
  transition: all 0.2s ease;
}

.icon-btn:hover {
  background: #e0f2fe;
  color: #0284c7;
  transform: translateY(-2px) scale(1.05);
  box-shadow: 0 4px 12px rgba(2, 132, 199, 0.15);
}

.icon-btn i {
  font-size: 18px;
}

/* 工具提示 */
.icon-btn::after {
  content: attr(data-tooltip);
  position: absolute;
  bottom: calc(100% + 8px);
  left: 50%;
  transform: translateX(-50%) translateY(4px);
  opacity: 0;
  pointer-events: none;
  white-space: nowrap;
  font-size: 12px;
  font-weight: 500;
  padding: 4px 8px;
  border-radius: 6px;
  background: rgba(15, 23, 42, 0.9);
  color: #fff;
  transition: opacity 0.15s ease, transform 0.15s ease;
}

.icon-btn:hover::after {
  opacity: 1;
  transform: translateX(-50%) translateY(0);
}

/* 响应式优化 */
@media (max-width: 520px) {
  .brand-text {
    font-size: 18px;
  }
  .icon-btn {
    width: 34px;
    height: 34px;
  }
}
</style>
