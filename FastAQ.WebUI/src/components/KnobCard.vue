<template>
    <div class="knob-card"> <!-- QA 卡片 -->
        <transition name="fade">
            <QACard v-if="showCard" :question="currentQA.question" :answer="currentQA.answer" class="QACard"/>
        </transition> <!-- 旋钮按钮（现代化图标风格） --> <button class="knob" @click="refreshCard" aria-label="随机"> <i
                class="fa-solid fa-rotate-right"></i> </button>
    </div>
</template>
<script
    lang="ts">    
    import QACard from "../components/QAcard.vue"; 
    export default { name: "KnobCard", components: { QACard }, 
                     data() { return { showCard: true, 
                     qaList: [{ question: "Vue3 中如何实现组件通信？", answer: "props、emits、provide/inject、Pinia 等" }, { question: "TypeScript 的优势？", answer: "类型安全，可维护" }, { question: "什么是 Vue 组件？", answer: "可复用的 UI 单元" }], currentQA: { question: "Vue3 中如何实现组件通信？", answer: "props、emits、provide/inject、Pinia 等" } }; }, methods: { refreshCard() { this.showCard = false; setTimeout(() => { const randomIndex = Math.floor(Math.random() * this.qaList.length); this.currentQA = this.qaList[randomIndex]; this.showCard = true; }, 500); } } }; </script>
<style
    scoped>
    @import url("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css");

    .knob-card {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 20px;
        width: 100%;
        max-width: 800px;
        margin: 0 auto;
        padding: 0 0;
    }

    /* 现代化旋钮按钮，参考 TopBar 的 icon-btn */
    .knob {
        position: relative;
        display: grid;
        place-items: center;
        width: 50px;
        height: 50px;
        border-radius: 12px;
        background: rgba(248, 250, 252, 0.85);
        border: 1px solid #e2e8f0;
        color: #0f172a;
        cursor: pointer;
        transition: all 0.2s ease;
        font-size: 20px;
    }

    .knob:hover {
        background: #e0f2fe;
        color: #0284c7;
        transform: translateY(-2px) scale(1.05);
        box-shadow: 0 4px 12px rgba(2, 132, 199, 0.15);
    }

    /* 淡入淡出动画 */
    .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.5s;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }

    /* 响应式优化 */
    @media (max-width: 520px) {
        .knob {
            width: 44px;
            height: 44px;
            font-size: 18px;
        }
    }
    .knob-card > .fade-enter-active,
.knob-card > .fade-leave-active {
    display: flex;
    justify-content: center;
    width: 100%;
}
</style>
