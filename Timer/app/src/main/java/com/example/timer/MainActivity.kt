package com.example.timer

import android.os.Bundle
import android.os.CountDownTimer
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.progressindicator.CircularProgressIndicator

class MainActivity : AppCompatActivity() {
    private var timer: CountDownTimer? = null
    private var remainingTime: Long = 0 // Оставшееся время в миллисекундах
    private var isPaused: Boolean = false // Флаг для отслеживания состояния паузы
    private lateinit var circularProgressIndicator: CircularProgressIndicator
    private lateinit var timerText: TextView
    private lateinit var startButton: Button
    private lateinit var pauseButton: Button
    private lateinit var inputHours: EditText
    private lateinit var inputMinutes: EditText
    private lateinit var inputSeconds: EditText

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        // Привязка элементов
        circularProgressIndicator = findViewById(R.id.circularProgressIndicator)
        timerText = findViewById(R.id.timerText)
        startButton = findViewById(R.id.startButton)
        pauseButton = findViewById(R.id.pauseButton)
        inputHours = findViewById(R.id.inputHours)
        inputMinutes = findViewById(R.id.inputMinutes)
        inputSeconds = findViewById(R.id.inputSeconds)

        startButton.setOnClickListener {
            val hours = inputHours.text.toString().toLongOrNull() ?: 0
            val minutes = inputMinutes.text.toString().toLongOrNull() ?: 0
            val seconds = inputSeconds.text.toString().toLongOrNull() ?: 0

            val totalSeconds = hours * 3600 + minutes * 60 + seconds
            if (totalSeconds > 0) {
                startTimer(totalSeconds)
            } else {
                timerText.text = "Введите положительное время!"
            }
        }

        pauseButton.setOnClickListener {
            if (isPaused) {
                resumeTimer() // Возобновить таймер
            } else {
                pauseTimer() // Приостановить таймер
            }
        }
    }

    private fun startTimer(totalSeconds: Long) {
        remainingTime = totalSeconds * 1000
        circularProgressIndicator.progress = 0
        timer?.cancel() // Сбрасываем предыдущий таймер, если есть

        isPaused = false // Устанавливаем флаг в false

        timer = object : CountDownTimer(remainingTime, 1000) {
            override fun onTick(millisUntilFinished: Long) {
                remainingTime = millisUntilFinished
                val progress = ((remainingTime * 100) / (totalSeconds * 1000)).toInt()
                circularProgressIndicator.progress = progress

                // Форматируем оставшееся время
                val hours = remainingTime / 3600000
                val minutes = (remainingTime % 3600000) / 60000
                val seconds = (remainingTime % 60000) / 1000
                val displayTime = String.format("%02d:%02d:%02d", hours, minutes, seconds)

                // Обновляем текст таймера
                timerText.text = displayTime
            }

            override fun onFinish() {
                circularProgressIndicator.progress = 100
                timerText.text = "End!"
                resetTimer() // Сбрасываем таймер
            }
        }.start()
    }

    private fun pauseTimer() {
        timer?.cancel() // Останавливаем таймер
        isPaused = true // Устанавливаем флаг паузы
        pauseButton.text = "Продолжить" // Изменяем текст кнопки
    }

    private fun resumeTimer() {
        startTimer(remainingTime / 1000) // Возобновляем таймер с оставшимся временем
        isPaused = false // Сбрасываем флаг паузы
        pauseButton.text = "Пауза" // Изменяем текст кнопки
    }

    private fun resetTimer() {
        remainingTime = 0
        timerText.text = "00:00:00"
        circularProgressIndicator.progress = 0
        pauseButton.text = "Пауза" // Возвращаем текст кнопки к исходному состоянию
    }

    override fun onDestroy() {
        super.onDestroy()
        timer?.cancel() // Отменяем таймер, если активен
    }
}
